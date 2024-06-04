using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class PlayerSystemInputHandler : MonoBehaviour
    {
        public delegate void ButtonPressed();

        public static event ButtonPressed OnEscapePressed;
        public static event ButtonPressed OnDialogSkip;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                OnDialogSkip?.Invoke();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnEscapePressed?.Invoke();
            }
        }
    }
}