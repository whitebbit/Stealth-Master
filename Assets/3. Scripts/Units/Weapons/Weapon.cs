using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public abstract class Weapon: MonoBehaviour, IWeapon
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        private float lastAttackTime;
        
        public void Attack(IWeaponVisitor visitor)
        {
            if (!(Time.time - lastAttackTime >= attackCooldown)) return;
            
            lastAttackTime = Time.time;
            
            PerformAttack();
            DoAnimation();
            PlaySound();
            CreateParticle();
            DoDamage(visitor);
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

        protected virtual void DoDamage(IWeaponVisitor visitor)
        {
            visitor.Visit(damage);
        }
    }
}