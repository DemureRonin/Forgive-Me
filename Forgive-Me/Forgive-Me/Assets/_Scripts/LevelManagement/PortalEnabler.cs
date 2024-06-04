using _Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.LevelManagement
{
    public class PortalEnabler : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onAllItemsGiven;
        private bool _dolphinDisabled;
        private bool _columnDisabled;

        private void Check()
        {
            if (_columnDisabled &&_dolphinDisabled )_onAllItemsGiven?.Invoke();
        }

        private void OnColumnDisabled()
        {
            _columnDisabled = true;
            Check();
        }

        private void OnDolphinDisabled()
        {
            _dolphinDisabled = true;
            Check();
        }
        private void OnEnable()
        {
            IconWidget.OnColumnDisabled += OnColumnDisabled;
            IconWidget.OnDolphinDisabled += OnDolphinDisabled;
        }

       
        private void OnDisable()
        {
            IconWidget.OnColumnDisabled -= OnColumnDisabled;
            IconWidget.OnDolphinDisabled -= OnDolphinDisabled;
        }
    }
}