using System;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Interfaces;

namespace _3._Scripts.Units.Damageable
{
    public class UnitDamageable: IDamageable
    {
        private readonly UnitHealth health;
        public event Action<float> OnDamageApplied; 
        
        public UnitDamageable(UnitHealth health)
        {
            this.health = health;
        }

        public void ApplyDamage(float damage)
        {
            if(!DamageNotNull(damage))
                return;
            
            health.Health -= damage;
            OnDamageAppliedEvent(damage);
        }

        private bool DamageNotNull(float damage)
        {
            return !(damage < 0);
        }

        private void OnDamageAppliedEvent(float damage)
        {
            OnDamageApplied?.Invoke(damage);
        }
    }
}