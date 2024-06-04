using System;
using _Scripts.Components.Triggers;
using UnityEngine;

namespace _Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private EnterEvent _onDamage;
        [SerializeField] private EnterEvent _onMultipliedDamage;
        [SerializeField] private EnterEvent _onDie;
        
        private int _damageDelta;
        private bool _isDamageMultiplied;
        public int Health => _health;
        private int _maxHp;

        public int MaxHp => _maxHp;

        private void Start()
        {
            SetMaxHp();
        }

        public void ModifyHealth(int damageDelta, GameObject actor, bool isDamageMultiplied)
        {
            _isDamageMultiplied = isDamageMultiplied;
            _health += damageDelta;
            _damageDelta = damageDelta;

            if (_health <= 0)
                _onDie?.Invoke(actor);
            if (_damageDelta >= 0) throw new Exception("Damage delta is more than zero");
            if (_isDamageMultiplied)
                _onMultipliedDamage?.Invoke(actor);
            else _onDamage?.Invoke(actor);
        }

        private void SetMaxHp()
        {
            _maxHp = _health;
        }
    }
}