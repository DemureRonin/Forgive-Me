using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    public class InputHandler : MonoBehaviour
    {
        private Player _player;
        private SphereCycle _sphereCycle;


        private void Start()
        {
            _player = GetComponent<Player>();
            _sphereCycle = GetComponent<SphereCycle>();
        }

        public void OnMoveButtonsPressed(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector3>();
            _player.OnMove(direction);
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (Time.timeScale == 0) return;
            StartCoroutine(_player.SphereAction());
        }

        public void OnShiftPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartCoroutine(_player.Dash());
            }
        }

        public void OnEPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _sphereCycle.CycleThroughSpheres();
            }
        }

        public void OnQPressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.Interact();
            }
        }
    }
}