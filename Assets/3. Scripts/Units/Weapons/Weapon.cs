using System;
using _3._Scripts.Detectors;
using _3._Scripts.Detectors.Interfaces;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons.Interfaces;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public abstract class Weapon: MonoBehaviour, IWeapon
    { 
        [SerializeField] private string id;
        [Header("Base")]
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [Header("Animation")]
        [SerializeField] protected AnimatorOverrideController animatorController;
        [SerializeField] protected  UnitAnimator unitAnimator;
        
        protected BaseDetector<IWeaponVisitor> Detector;
        protected float LastAttackTime;

        public string ID => id;

        private void Awake()
        {
            Detector = GetComponent<BaseDetector<IWeaponVisitor>>();
        }
        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            Resetting();
        }
        
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

        protected abstract void Initialize();
        protected abstract void Resetting();
        
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