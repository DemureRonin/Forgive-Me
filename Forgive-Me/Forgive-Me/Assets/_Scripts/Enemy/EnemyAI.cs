using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float _runSpeed;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _walkBackThreshold;
        [SerializeField] private float _walkForwardThreshold;
        [SerializeField] private float _rotationSpeed;


        [Header("Combat")] [SerializeField] private float _attackSpeed;
        [SerializeField] private float _knockbackSpeed;
        [SerializeField] private Collider _vision;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private UnityEvent _onSeePlayer;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private AudioClip _anticipation;


        private EnemyGroup _group;

        private AudioSource _audioSource;
        private GameObject _player;
        private Rigidbody _rigidbody;
        private Animator _animator;
        private Coroutine _coroutine;

        private float _velocity;
        private float _chanceTick = 0.5f;
        private float _lookDelay = 0.8f;

        private bool _isAgro;
        private bool _isAttacking;
        private bool _canAttack;

        private int _moveModifier = -1;
        private int _animationModifier = -1;
        private int _successfulAttackChance;

        private WaitForSeconds _cooldown;

        private static readonly int RunAnimation = Animator.StringToHash("run");
        private static readonly int CooldownAnimation = Animator.StringToHash("cooldown");
        private static readonly int AttackAnimation = Animator.StringToHash("attack");
        private static readonly int MovementDirection = Animator.StringToHash("MovementDirection");
        private static readonly int Walking = Animator.StringToHash("Walk");
        private static readonly int Anticipation = Animator.StringToHash("Anticipation");
        private const string PlayerTag = "Player";

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _group = GetComponentInParent<EnemyGroup>();

            _cooldown = new WaitForSeconds(_cooldownTime);
            _rigidbody.isKinematic = true;
        }

        private void StartState(IEnumerator state)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _coroutine = StartCoroutine(state);
        }


        public void OnSeePlayer(GameObject player)
        {
            _onSeePlayer?.Invoke();
            _rigidbody.isKinematic = false;
            _vision.enabled = false;

            _player = player;
            _isAgro = true;

            StartCoroutine(LookAtPlayer());

            StartState(Walk());
        }

        private IEnumerator AttackChance()
        {
            while (true)
            {
                yield return new WaitForSeconds(_chanceTick);
                var rand = Random.Range(1, 15);
                if (rand < _successfulAttackChance)
                {
                    _animator.SetTrigger(Anticipation);
                    StartState(AnticipationState());
                   
                    _successfulAttackChance = 0;
                    yield break;
                }

                _successfulAttackChance++;
            }
        }

        private IEnumerator AnticipationState()
        {
            _rigidbody.isKinematic = true;
            yield return null;
        }

        private IEnumerator Walk()
        {
            StartCoroutine(AttackChance());
            _rigidbody.isKinematic = false;
            _animator.SetTrigger(Walking);
            const float threshold = 0.01f;
            while (true)
            {
                var enemyPlayerVector = _player.transform.position - transform.position;
                _velocity = Mathf.Clamp(_rigidbody.velocity.sqrMagnitude, 0, 1);
                if (enemyPlayerVector.sqrMagnitude >= _walkBackThreshold)
                {
                    _moveModifier = 1;
                    if (_velocity < threshold)
                    {
                        _animationModifier = 1;
                    }
                }
                else if (enemyPlayerVector.sqrMagnitude <= _walkForwardThreshold)
                {
                    _moveModifier = -1;

                    if (_velocity < threshold)
                    {
                        _animationModifier = -1;
                    }
                }
                else if (enemyPlayerVector.sqrMagnitude >= _walkForwardThreshold &&
                         enemyPlayerVector.sqrMagnitude <= _walkBackThreshold)
                {
                    if (_velocity < threshold)
                    {
                        _animationModifier = _moveModifier;
                    }
                }

                _rigidbody.AddForce(enemyPlayerVector.normalized * (_moveModifier * _walkSpeed) * Time.deltaTime,
                    ForceMode.Force);
                _animator.SetFloat(MovementDirection, _animationModifier * _velocity);
                yield return null;
            }
        }


        private IEnumerator LookAtPlayer()
        {
            yield return new WaitForSeconds(_lookDelay);
            while (_isAgro && !_isAttacking)
            {
                var playerPosition = _player.transform.position;
                var positionSelf = transform.position;
                var vector3 = new Vector3(playerPosition.x - positionSelf.x, 0, playerPosition.z - positionSelf.z);


                var rotation = Quaternion.LookRotation(vector3, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
                yield return null;
            }
        }

        private void StartRunning()
        {
            StartState(RunToPlayer());
        }

        private IEnumerator RunToPlayer()
        {
            _rigidbody.isKinematic = false;
            _animator.Play(RunAnimation);
            while (_isAgro && !_isAttacking)
            {
                var vector = (_player.transform.position - transform.position).normalized;
                _rigidbody.AddForce(vector * _runSpeed * Time.deltaTime, ForceMode.Force);
                yield return null;
            }
        }

        public void OnPlayerInRange()
        {
            _canAttack = true;
            StartState(Attack());
        }

        public void OnPlayerOutOfRange()
        {
            _canAttack = false;
        }

        private void SetAttackState()
        {
            _isAttacking = !_isAttacking;
            if (_isAttacking)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.AddForce(
                    (_player.transform.position - transform.position).normalized * _attackSpeed * Time.deltaTime,
                    ForceMode.Impulse);
                StopCoroutine(LookAtPlayer());
            }
            else
            {
                _rigidbody.isKinematic = true;
            }
        }

        private void OnAnticipation()
        {
            _audioSource.PlayOneShot(_anticipation);
        }

        private IEnumerator Attack()
        {
            while (_canAttack)
            {
                _rigidbody.isKinematic = true;
                _audioSource.PlayOneShot(_attack);
                _animator.Play(AttackAnimation);
                yield return _cooldown;
            }

            _animator.Play(CooldownAnimation);
            StartCoroutine(LookAtPlayer());
            yield return new WaitForSeconds(Random.Range(1, 2));
            StartState(Walk());
        }

        public void KnockBackTarget(GameObject other)
        {
            if (!other.CompareTag(PlayerTag)) return;
            var otherRigidbody = other.GetComponent<Rigidbody>();
            otherRigidbody.AddForce((_player.transform.position - transform.position).normalized * _knockbackSpeed,
                ForceMode.Impulse);
        }

        public void OnDie()
        {
            _group.OnEnemyDeath();
        }
    }
}