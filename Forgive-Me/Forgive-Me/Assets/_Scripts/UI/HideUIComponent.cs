using System;
using UnityEngine;

namespace _Scripts.UI
{
    public class HideUIComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _ui;
        private bool _enabled = true;
        
        private void ToggleUI()
        {
            _enabled = !_enabled;
            _ui.SetActive(_enabled);
        }

        private void OnEnable()
        {
            UIHideObserver.OnToggleUI += ToggleUI;
        }

        private void OnDisable()
        {
            UIHideObserver.OnToggleUI -= ToggleUI; 
        }
    }
}