using System;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Player;
using UnityEngine;

namespace _3._Scripts.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable, IDying
    {
        [Header("Unit Settings")]
        [SerializeField] private int maxHealth = 100;
        public UnitHealth Health { get; private set; }
        public Transform LastDamageDealer { get; set; }
        private void Awake()
        {
            Health = new UnitHealth(maxHealth);
            OnAwake();
        }

        private void Start()
        {
            Health.OnHealthChanged += (current, _) =>
            {
                if (current <= 0) Dead();
            };
            OnStart();
        }

        public virtual void ApplyDamage(float damage)
        {
            Health.Health -= damage;
        }

        public virtual void Dead()
        {
            
        }

        protected virtual void OnAwake()
        {
            
        }
        protected virtual void OnStart()
        {
            
        }
    }
}