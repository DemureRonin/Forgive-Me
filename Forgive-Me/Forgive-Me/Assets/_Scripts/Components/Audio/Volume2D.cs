using System;
using _Scripts.Model;
using _Scripts.PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts.Components.Audio
{
    public class Volume2D : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _minDistance = 1;
        [SerializeField] private float _maxDistance = 20;
        [SerializeField] private float _maxValue = 1;
        [SerializeField] private PlayerPersistentSettings _settings;

        private Transform _listenerTransform;

        private void Start()
        {
            _listenerTransform = FindAnyObjectByType<Player>().transform;
        }

        private void Update()
        {
            var dist = Vector3.Distance(transform.position, _listenerTransform.position);

            if (dist < _minDistance)
            {
                _audioSource.volume = 1;
            }
            else if (dist > _maxDistance)
            {
                _audioSource.volume = 0;
            }
            else
            {
                _audioSource.volume = 1 - ((dist - _minDistance) / (_maxDistance - _minDistance));
            }

            _audioSource.volume = Mathf.Clamp(_audioSource.volume, 0, _maxValue) * _settings.SfxVolume;
        }
    }
}