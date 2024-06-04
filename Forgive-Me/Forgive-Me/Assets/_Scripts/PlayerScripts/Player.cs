using System.Collections;
using _Scripts.Camera;
using _Scripts.Components;
using _Scripts.Components.Audio;
using _Scripts.Model;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private Transform _orientation;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _groundDrag;
        [SerializeField] private float _airMultiplier;
        [SerializeField] private float _airSpeed;
        [SerializeField] private float _slopeMovementSpeed;
        [SerializeField] private float _maxSpeedController = 2f;
        [SerializeField] private float _maxAirSpeedController = 1f;

        [Header("Dash")] [SerializeField] private float _dashForce;
        [SerializeField] private float _dashCooldown;

        [Header("Sphere")] private GameObject _shootingSphere;
        private GameObject _shootingPosition;
        private SkinnedMeshRenderer _sphereHolderMeshRenderer;
        private GameObject _sphereProjectile;
        private bool _isReadyToShoot = true;
        private bool _isIcoSphereActive;
        private bool _cooldown;

        private GameSession _session;
        private GroundCheck _groundCheck;
        private Rigidbody _rigidbody;

        private Vector3 _inputDirection;
        private Vector3 _moveDirection;
        private SphereCycle _sphereCycle;

        private bool _isDashingAllowed = true;

        public bool IsReadyToShoot => _isReadyToShoot;
        private const string SphereHolder = "SphereHolder";

        private PlayAudioComponent _playAudioComponent;

        private PlayerPerksModel Perks => _session.Data.PlayerPerks;
        private InventoryItemModel Inventory => _session.Data.Inventory;

        public delegate void ActionEvent(float value);

        public static event ActionEvent OnDash;

        public delegate void PlayerEvent();

        public static event ActionEvent OnPlayerDeath;
        public static event PlayerEvent OnInteraction;
        public static event PlayerEvent OnRoundSphereReturn;

        private void Awake()
        {
            _groundCheck = GetComponent<GroundCheck>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.freezeRotation = true;
            _playAudioComponent = GetComponent<PlayAudioComponent>();
        }

        private void Start()
        {
            _shootingPosition = FindFirstObjectByType<ShootingPosition>().gameObject;
            _session = FindFirstObjectByType<GameSession>();

            var sphereHolder = GameObject.FindGameObjectWithTag(SphereHolder);
            _sphereHolderMeshRenderer = sphereHolder.GetComponent<SkinnedMeshRenderer>();
            _sphereCycle = FindFirstObjectByType<SphereCycle>();
        }

        public void SetShootingSphere(GameObject sphere)
        {
            _shootingSphere = sphere;
        }


        public void OnDie()
        {
            OnPlayerDeath?.Invoke(4);
        }

        private void Update()
        {
            SetDrag();
        }

        private void FixedUpdate()
        {
            ControlSpeed();
            Move();
        }

        private void Move()
        {
            _moveDirection = _orientation.forward * _inputDirection.z + _orientation.right * _inputDirection.x;
            CheckSurface();
        }

        private void SetDrag()
        {
            if (_groundCheck.IsGrounded() || _groundCheck.IsOnSlope())
                _rigidbody.drag = _groundDrag;
            else _rigidbody.drag = 0;
        }

        private void CheckSurface()
        {
            if (_groundCheck.IsOnSlope())
            {
                _rigidbody.AddForce(OnSlopeMovement() * _slopeMovementSpeed, ForceMode.Force);
            }

            if (_groundCheck.IsGrounded())
            {
                _rigidbody.AddForce(_moveDirection.normalized * _moveSpeed, ForceMode.Force);
            }
            else
            {
                _rigidbody.AddForce(_moveDirection.normalized * (_moveSpeed * _airSpeed), ForceMode.Force);
            }
        }

        private Vector3 OnSlopeMovement()
        {
            return Vector3.ProjectOnPlane(_moveDirection, _groundCheck.SlopeHit.normal).normalized;
        }

        private void ControlSpeed()
        {
            if (_groundCheck.IsGrounded())
            {
                var velocity = _rigidbody.velocity;
                var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);
                if (!(flatVelocity.magnitude > _moveSpeed / _maxSpeedController)) return;
                var limitedVelocity = flatVelocity.normalized * _moveSpeed / _maxSpeedController;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
                return;
            }

            var airVelocity = _rigidbody.velocity;
            var flatAirVelocity = new Vector3(airVelocity.x, 0f, airVelocity.z);
            if (!(flatAirVelocity.magnitude > _moveSpeed / _maxAirSpeedController)) return;
            var limitedAirVelocity =
                flatAirVelocity.normalized * (_moveSpeed * _airMultiplier) / _maxAirSpeedController;
            _rigidbody.velocity = new Vector3(limitedAirVelocity.x, _rigidbody.velocity.y, limitedAirVelocity.z);
        }

        public void OnMove(Vector3 direction)
        {
            _inputDirection = direction;
        }


        public IEnumerator SphereAction()
        {
            if (Inventory.Spheres.Count < 0 || !Perks.IsSphereAllowed) yield break;
            if (_cooldown) yield break;
            _cooldown = true;

            switch (_sphereCycle.CurrentCount)
            {
                case 0:
                    RoundSphereAction();
                    break;
                case 1:
                    IcoSphereAction();
                    break;
                case 2:
                    DodecoSphereAction();
                    break;
            }

            if (!_isReadyToShoot)
                yield return new WaitForSeconds(0.5f);
            _cooldown = false;
        }

        private void RoundSphereAction()
        {
            RoundSphereBehaviour();
        }

        private void RoundSphereBehaviour()
        {
            if (_isReadyToShoot)
            {
                _playAudioComponent.Play("ThrowRound");
                _sphereProjectile =
                    Instantiate(_shootingSphere, _shootingPosition.transform.position, Quaternion.identity);
                _isReadyToShoot = false;
                _sphereHolderMeshRenderer.enabled = false;
                return;
            }

            _isReadyToShoot = true;
            _sphereHolderMeshRenderer.enabled = true;
            _isIcoSphereActive = false;
            _playAudioComponent.Play("Return");
            OnRoundSphereReturn?.Invoke();
        }

        private void IcoSphereAction()
        {
            if (_isIcoSphereActive)
            {
                _playAudioComponent.Play("Fail");
                return;
            }

            _sphereProjectile =
                Instantiate(_shootingSphere, _shootingPosition.transform.position, Quaternion.identity);
            _playAudioComponent.Play("ThrowIco");
            _sphereCycle.CycleThroughSpheres();
            _isIcoSphereActive = true;
        }

        private void DodecoSphereAction()
        {
            RoundSphereBehaviour();
        }

        public IEnumerator Dash()
        {
            if (!_groundCheck.IsGrounded() || _groundCheck.IsOnSlope() || !_isDashingAllowed ||
                !Perks.IsDashingUnlocked) yield break;
            OnDash?.Invoke(_dashCooldown);
            _isDashingAllowed = false;

            var vector = _moveDirection * _dashForce;
            _rigidbody.AddForce(vector * _dashForce, ForceMode.Impulse);

            yield return new WaitForSeconds(_dashCooldown);
            _rigidbody.velocity = Vector3.zero;
            _isDashingAllowed = true;
        }

        public void Interact()
        {
            OnInteraction?.Invoke();
        }

        private void OnEnable()
        {
            _cooldown = false;
            _isDashingAllowed = true;
        }
    }
}