using _Scripts.UI.Menu;
using UnityEngine;

namespace _Scripts.Model
{
    [CreateAssetMenu(fileName = "PlayerSettings")]
    public class PlayerPersistentSettings : ScriptableObject
    {
        [SerializeField] private float _sensX;
        [SerializeField] private float _sensY;
        [SerializeField] private float _musicVolume;
        [SerializeField] private float _sFXVolume;

        public float SensX => _sensX;

        public float SensY => _sensY;

        public float MusicVolume => _musicVolume;

        public float SfxVolume => _sFXVolume;

        private void SetSensitivity(float value)
        {
            _sensX = value;
            _sensY = value;
        }

        private void SetSfxVolume(float value)
        {
            _sFXVolume = value;
        }

        private void SetMusicVolume(float value)
        {
            _musicVolume = value;
        }

        private void OnEnable()
        {
            MenuWindow.OnSensitivityChanged += SetSensitivity;
            MenuWindow.OnMusicVolumeChanged += SetMusicVolume;
            MenuWindow.OnSfxVolumeChanged += SetSfxVolume;
        }

        private void OnDisable()
        {
            MenuWindow.OnSensitivityChanged -= SetSensitivity;
            MenuWindow.OnMusicVolumeChanged -= SetMusicVolume;
            MenuWindow.OnSfxVolumeChanged -= SetSfxVolume;
        }
    }
}