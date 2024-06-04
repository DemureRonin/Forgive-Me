using _Scripts.Camera;
using _Scripts.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    public class InputToggle : MonoBehaviour
    {
      
        
        private PlayerInput _playerInput;
        private FirstPersonCamera _cameraMovement;
        public delegate void InputEvent();

        public static event InputEvent OnDashingEnabled;
        public static event InputEvent OnSphereEnabled;
        public static event InputEvent OnSphereDisabled;

        private void Start()
        {
            _playerInput = FindFirstObjectByType<PlayerInput>();
            _cameraMovement = FindFirstObjectByType<FirstPersonCamera>();
        }

        public void EnableDashing()
        {
            OnDashingEnabled?.Invoke();
        }

        public void DisableSphere()
        {
            OnSphereDisabled?.Invoke();
        }

        public void EnableSphere()
        {
            OnSphereEnabled?.Invoke();
        }

        public void EnableInput()
        {
            _playerInput.enabled = true;
        }

        public void DisableInput()
        {
            _playerInput = FindFirstObjectByType<PlayerInput>();
            _playerInput.enabled = false;
        }

        public void EnableCameraMovement()
        {
            _cameraMovement.enabled = true;
        }

        public void DisableCameraMovement()
        {
            _cameraMovement = FindFirstObjectByType<FirstPersonCamera>();
            _cameraMovement.enabled = false;
        }
    }
}