using System;
using _Scripts.Camera;
using _Scripts.Effects;
using _Scripts.Model;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.LevelManagement
{
    [RequireComponent(typeof(Spawner))]
    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private UnityEvent _onCheck;
        [SerializeField] private int _number;

        private GameSession _session;

        public string ID => _id;

        private void Awake()
        {
            _spawner = GetComponent<Spawner>();
        }

        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
        
        }

        public void Check()
        {
            _session.SetChecked(_id);
        }

        public void SpawnHero()
        {
            _onCheck?.Invoke();
            _spawner.Spawn(_spawnPosition.position, _spawnPosition.localRotation);
   
        }
    }
}