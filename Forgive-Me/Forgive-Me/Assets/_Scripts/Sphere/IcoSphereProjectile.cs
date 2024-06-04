using _Scripts.Components.Health;
using _Scripts.Effects;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Sphere
{
    public class IcoSphereProjectile : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _speed;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private int _baseDamageDelta;
        [SerializeField] private float _explosionRadius;

        [SerializeField] private LayerMask _rayCastLayers;
        [SerializeField] private LayerMask _explosionLayers;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private GameObject _visualRadius;

        private readonly Collider[] _enemiesHit = new Collider[10];

        private UnityEngine.Camera _camera;

        private bool _allowedToMove;
        private Vector3 _targetPoint;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (!_allowedToMove) return;
            if (transform.position != _targetPoint)
            {
                transform.position = Vector3.Lerp(transform.position, _targetPoint, _speed);
            }
        }

        private void Start()
        {
            ShootSphere();
            SetExplosionRadius(0);
        }

        private void SetExplosionRadius(int bounceCount)
        {
            var multiplier = bounceCount + 1;
            var size = _explosionRadius * multiplier * 0.5f;
            _explosionRadius = _explosionRadius * multiplier;
            _visualRadius.transform.localScale = new Vector3(size,size,size );
        }

        private void ShootSphere()
        {
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            var targetPoint =
                Physics.Raycast(ray, out var hit, _maxDistance, _rayCastLayers, QueryTriggerInteraction.Ignore)
                    ? hit.point
                    : ray.GetPoint(_maxDistance);
            _targetPoint = targetPoint + Vector3.one;
            _allowedToMove = true;
        }

        private void Explode()
        {
            var collidersNum = Physics.OverlapSphereNonAlloc(
                transform.position,
                _explosionRadius,
                _enemiesHit,
                _explosionLayers,
                QueryTriggerInteraction.Collide);

            var sphereProjectile = FindAnyObjectByType<RoundSphereProjectile>();
            var damage = -(sphereProjectile.BounceCount + 1);
            for (var i = 0; i < collidersNum; i++)
            {
                var enemyHealth = _enemiesHit[i].gameObject.GetComponent<HealthComponent>();
                enemyHealth.ModifyHealth(damage, enemyHealth.gameObject, true );
            }

            _spawner.Spawn(transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        

        private void OnEnable()
        {
            Player.OnRoundSphereReturn += Explode;
            RoundSphereProjectile.OnBounce += SetExplosionRadius;
        }


        private void OnDisable()
        {
            Player.OnRoundSphereReturn -= Explode;
            RoundSphereProjectile.OnBounce -= SetExplosionRadius;
        }
    }
}