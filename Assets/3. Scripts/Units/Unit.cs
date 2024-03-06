using System;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Player;
using UnityEngine;

namespace _3._Scripts.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable, IDying
    {
        [SerializeField] private int maxHealth = 100;
        public UnitHealth Health { get; set; }

        private void Awake()
        {
            Health = new UnitHealth(maxHealth);
        }

        private void Start()
        {
            Health.OnHealthChanged += (current, _) =>
            {
                if (current <= 0) Dead();
            };
        }

        public virtual void ApplyDamage(float damage)
        {
            Health.Health -= damage;
        }

        public virtual void Dead()
        {
            Destroy(gameObject);
        }
    }
}