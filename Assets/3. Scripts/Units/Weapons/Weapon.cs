using _3._Scripts.Units.Weapons.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public abstract class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float attackCooldown;
        private float lastAttackTime;
        
        public void Attack()
        {
            if (!(Time.time - lastAttackTime >= attackCooldown)) return;
            
            lastAttackTime = Time.time;
            
            PerformAttack();
            DoAnimation();
            PlaySound();
            CreateParticle();
        }

        protected virtual void PerformAttack()
        {
            
        }
        
        protected virtual void PlaySound()
        {
            
        }
        
        protected virtual void CreateParticle()
        {
            
        }
        
        protected virtual void DoAnimation()
        {
            
        }
    }
}