using System.Collections.Generic;
using _Scripts.Components;
using _Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.PlayerScripts
{
    public class SphereCycle : MonoBehaviour
    {
        [SerializeField] private Material _sphereMaterial;
        [SerializeField] private List<Color> _colors;
        [SerializeField] private Color _defaultColor;

        private readonly HashSet<GameObject> _spheresAvailable = new();

        private Player _player;
        private GameSession _session;
        private GameObject _sphere;
        private Animator _sphereAnimator;

        private int _currentCount;
        public int CurrentCount => _currentCount;

        private int _maxCountValue;

        private static readonly int Morph = Animator.StringToHash("Morph");
        private static readonly int Count = Animator.StringToHash("Count");

        private const string SphereHolder = "SphereHolder";
        private InventoryItemModel Inventory => _session.Data.Inventory;

        private void Start()
        {
            _player = GetComponent<Player>();
            _session = FindFirstObjectByType<GameSession>();

            var sphereHolder = GameObject.FindGameObjectWithTag(SphereHolder);
            _sphereAnimator = sphereHolder.GetComponent<Animator>();

            _sphere = Inventory.Spheres[_currentCount];
            _player.SetShootingSphere(_sphere);
            CountSpheres();
        }

        private void CountSpheres()
        {
            foreach (var sphere in Inventory.Spheres)
            {
                if (sphere != null)
                    _spheresAvailable.Add(sphere);
            }

            _maxCountValue = _spheresAvailable.Count - 1;
        }

        public void CycleThroughSpheres()
        {
            if (!_player.IsReadyToShoot || Inventory.Spheres.Count == 1) return;
            
            if (_currentCount == _maxCountValue)
                _currentCount = 0;
            else _currentCount++;

            _sphereMaterial.color = _colors[_currentCount];
            _sphere = Inventory.Spheres[_currentCount];
            _player.SetShootingSphere(_sphere);
            _sphereAnimator.SetTrigger(Morph);
            _sphereAnimator.SetInteger(Count, _currentCount);
        }

        private void SetDefaultColor(Scene arg0, LoadSceneMode arg1)
        {
            _sphereMaterial.color = _defaultColor;
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SetDefaultColor;
            ItemReceiver.OnSphereReceived += CountSpheres;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SetDefaultColor;
            ItemReceiver.OnSphereReceived -= CountSpheres;
        }
    }
}