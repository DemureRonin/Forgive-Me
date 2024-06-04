using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Enemy
{
    public class ShootingEnemyAI : MonoBehaviour
    {
        [SerializeField] private Collider _vision;
        [SerializeField] private float _cooldownTime;
        [SerializeField] private UnityEvent _onSeePlayer;
        [SerializeField] private AudioClip _attack;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _shootingPosition;

        private EnemyGroup _group;

        private AudioSource _audioSource;
        private GameObject _player;
        private Animator _animator;
        private Coroutine _coroutine;

        private bool _isAgro;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _group = GetComponentInParent<EnemyGroup>();
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
            _vision.enabled = false;

            _player = player;
            _isAgro = true;

            StartCoroutine(LookAtPlayer());
            StartCoroutine(AttackChance());
        }

        private IEnumerator LookAtPlayer()
        {
            while (_isAgro)
            {
                var playerPosition = _player.transform.position;
                var positionSelf = transform.position;
                var vector3 = new Vector3(playerPosition.x - positionSelf.x, 0, playerPosition.z - positionSelf.z);

                transform.rotation = Quaternion.LookRotation(vector3, Vector3.up);
                yield return null;
            }
        }

        private IEnumerator AttackChance()
        {
            while (_isAgro)
            {
                _animator.Play("shootProjectile");

                yield return new WaitForSeconds(Random.Range(2, 5));
            }
        }

        private void Shoot()
        {
            _audioSource.PlayOneShot(_attack);
            Instantiate(_projectile, _shootingPosition.position, Quaternion.identity);
        }

        public void OnDie()
        {
            _group.OnEnemyDeath();
        }
    }
}