using System.Collections.Generic;
using _Scripts.LevelManagement;
using _Scripts.Model;
using _Scripts.PlayerScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.UI.Menu
{
    public class MenuWindow : AnimatedWindow
    {
        [SerializeField] private PlayerPersistentSettings _playerPersistentSettings;
        [SerializeField] private Slider _sensSlider;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private SceneReloader _sceneReloader;
        [SerializeField] private Button _restartButton;
        private readonly string[] _restartButtonEnabledScenes = {"Prologue2", "Level1", "Level2", "Level3"};
        private bool _isOpen;
        private bool _paused;
        private AudioSource[] _audioSources;

        public delegate void ValueChanged(float value);

        public static event ValueChanged OnSensitivityChanged;
        public static event ValueChanged OnMusicVolumeChanged;
        public static event ValueChanged OnSfxVolumeChanged;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            _sensSlider.value = _playerPersistentSettings.SensX;
            _soundSlider.value = _playerPersistentSettings.SfxVolume;
            _musicSlider.value = _playerPersistentSettings.MusicVolume;

            foreach (var scene in _restartButtonEnabledScenes)
            {
                if (SceneManager.GetActiveScene().name != scene) continue;
                _restartButton.interactable = true;
                break;
            }
        }

        public void ChangeSensitivity()
        {
            OnSensitivityChanged?.Invoke(_sensSlider.value);
        }

        public void ChangeMusicVolume()
        {
            foreach (var audioSource in _audioSources)
            {
                if (!audioSource.CompareTag("Music")) continue;
                audioSource.volume = _musicSlider.value;
            }

            OnMusicVolumeChanged?.Invoke(_musicSlider.value);
        }

        public void ChangeSfxVolume()
        {
            foreach (var audioSource in _audioSources)
            {
                if (audioSource.CompareTag("Music")) continue;
                audioSource.volume = _soundSlider.value;
            }

            OnSfxVolumeChanged?.Invoke(_soundSlider.value);
        }

        private void ToggleMenu()
        {
            ToggleInGameTime();
            _isOpen = !_isOpen;
            if (_isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                ShowWindow();
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            HideWindow();
        }

        private void OnEnable()
        {
            PlayerSystemInputHandler.OnEscapePressed += ToggleMenu;
        }

        private void OnDisable()
        {
            PlayerSystemInputHandler.OnEscapePressed -= ToggleMenu;
        }

        private void ToggleInGameTime()
        {
            _paused = !_paused;
            if (_paused)
            {
                Time.timeScale = 0f;
                return;
            }

            Time.timeScale = 1f;
        }

        public void Restart()
        {
            Time.timeScale = 1;
            _sceneReloader.ReloadSceneTime(0);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}