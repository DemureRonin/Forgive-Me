using UnityEngine;

namespace _Scripts.UI
{
    public class UIHideObserver : MonoBehaviour
    {
        [SerializeField] private bool _onStart;

        public delegate void UIEvent();

        public static event UIEvent OnToggleUI;

        private void Start()
        {
            if (_onStart)
                ToggleUI();
        }


        public void ToggleUI()
        {
            OnToggleUI?.Invoke();
        }
    }
}