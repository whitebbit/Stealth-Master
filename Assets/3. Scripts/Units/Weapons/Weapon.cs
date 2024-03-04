using System;
using _3._Scripts.Detectors;
using _3._Scripts.Detectors.Interfaces;
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
        
        [SerializeField] protected BaseDetector<IWeaponVisitor> detector;
        protected float LastAttackTime;

       

        public virtual void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;
            
            LastAttackTime = Time.time;
            
            PerformAttack();
            DoAnimation();
            PlaySound();
            CreateParticle();
            DoDamage(visitor);
        }

        protected bool CanAttack()
        {
            return Time.time - LastAttackTime < attackCooldown;
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