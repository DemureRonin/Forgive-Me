using _Scripts.Camera;
using _Scripts.Components.Audio;
using _Scripts.Model;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Sphere
{
    public class RoundSphereProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _returnSpeed;
        [SerializeField] private LayerMask _rayCastLayers;
        [SerializeField] private PlayerPersistentSettings _settings;

        private PlayAudioComponent _playAudioComponent;
        private AudioSource _audioSource;
        private ShootingPosition _shootingPosition;
        private Rigidbody _rigidbody;
        private UnityEngine.Camera _camera;

        private float _maxDistance = 100f;
        private bool _isReturning;
        private int _bounceCount;

        public int BounceCount => _bounceCount;

        public bool IsReturning => _isReturning;
        public delegate void RoundSphereEvent(int value);

        public static event RoundSphereEvent OnBounce;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _rigidbody = GetComponent<Rigidbody>();
            _shootingPosition = FindAnyObjectByType<ShootingPosition>();
            _playAudioComponent = GetComponent<PlayAudioComponent>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _settings.SfxVolume;
        }

        private void FixedUpdate()
        {
            ReturnSphere();
        }

        private void Start()
        {
            ShootSphere();
        }

        public void IncreaseBounceCount()
        {
            if (_isReturning) return;
            _bounceCount++;
            if (_bounceCount > 3) return;
            _playAudioComponent.Play(_bounceCount.ToString());
            OnBounce?.Invoke(_bounceCount);
        }

        private void ShootSphere()
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            var targetPoint =
                Physics.Raycast(ray, out var hit, _maxDistance, _rayCastLayers, QueryTriggerInteraction.Ignore)
                    ? hit.point
                    : ray.GetPoint(_maxDistance);
            var direction = targetPoint - transform.position;

            _rigidbody.AddForce(direction.normalized * _speed, ForceMode.Impulse);
        }

        private void RoundSphereReturn()
        {
            _rigidbody.velocity = Vector3.zero;
            _isReturning = true;
        }

        private void ReturnSphere()
        {
            if (!_isReturning) return;
            transform.position =
                Vector3.MoveTowards(transform.position, _shootingPosition.transform.position, _returnSpeed * Time.deltaTime);
            if (transform.position == _shootingPosition.transform.position)
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            Player.OnRoundSphereReturn += RoundSphereReturn;
        }

        private void OnDisable()
        {
            Player.OnRoundSphereReturn -= RoundSphereReturn;
        }
    }
}