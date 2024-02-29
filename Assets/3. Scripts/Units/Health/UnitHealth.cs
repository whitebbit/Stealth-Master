using System;

namespace _3._Scripts.Units.Health
{
    public class UnitHealth
    {
        public event Action<float, float> OnHealthChanged;

        private readonly float maxHealth;
        private float currentHealth;

        public float Health
        {
            get => currentHealth;
            set => SetHealth(value);
        }

        public UnitHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }

        private void SetHealth(float value)
        {
            if (Health <= 0)
                return;
            
            currentHealth = Math.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }
    }
}