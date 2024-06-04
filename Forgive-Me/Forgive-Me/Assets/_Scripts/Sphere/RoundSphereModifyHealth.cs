using _Scripts.Components.Health;
using UnityEngine;

namespace _Scripts.Sphere
{
    [RequireComponent(typeof(RoundSphereProjectile))]
    public class RoundSphereModifyHealth : ModifyHealthComponent
    {
        [SerializeField] private int _backHitModifier = 2;

        private RoundSphereProjectile _roundSphereProjectile;
        private int _baseDamage;

        private void Awake()
        {
            _roundSphereProjectile = GetComponent<RoundSphereProjectile>();
            _baseDamage = _hpDelta;
        }

        public override void ModifyHealth(GameObject recipient)
        {
            var bounceModifier = -_roundSphereProjectile.BounceCount + _baseDamage;
            _hpDelta = _roundSphereProjectile.IsReturning ? bounceModifier * _backHitModifier : bounceModifier;
            IsDamageMultiplied = _roundSphereProjectile.IsReturning;
            base.ModifyHealth(recipient);
        }
    }
}