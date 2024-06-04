using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.UI.Widgets
{
    public class HealthChangeObserver : MonoBehaviour
    {
        private HealthComponent _healthComponent;

        public delegate void HealthEvent(int currentHp, int maxHp);

        public static event HealthEvent OnHealthChanged;

        private void Awake()
        {
        _healthComponent = GetComponent<HealthComponent>();
        }

        public void ObserveHealthChanged()
        {
            var maxHp = _healthComponent.MaxHp;
            var currentHp = _healthComponent.Health;
            OnHealthChanged?.Invoke(currentHp, maxHp);
        }
    }
}