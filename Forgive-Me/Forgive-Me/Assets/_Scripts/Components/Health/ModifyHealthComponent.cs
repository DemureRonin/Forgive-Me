using System.Collections;
using UnityEngine;

namespace _Scripts.Components.Health
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] protected int _hpDelta = -1;
        protected bool IsDamageMultiplied;
        private bool _canDamage = true;

        public virtual void ModifyHealth(GameObject recipient)
        {
            if (!_canDamage) return;
            _canDamage = false;
            var healthComponent = recipient.GetComponent<HealthComponent>();
            healthComponent.ModifyHealth(_hpDelta, gameObject, IsDamageMultiplied);
            StartCoroutine(DamageCooldown());
        }

        private IEnumerator DamageCooldown()
        {
            yield return new WaitForSeconds(0.2f);
            _canDamage = true;
        }

        private void OnEnable()
        {
            _canDamage = true;

        }
    }
}