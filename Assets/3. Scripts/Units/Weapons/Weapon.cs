using System;
using _3._Scripts.Detectors;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons.Interfaces;
using _3._Scripts.Units.Weapons.Scriptable;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] protected WeaponData data;

        [Header("Components")] [SerializeField]
        protected UnitAnimator unitAnimator;

        protected float LastAttackTime;
        public event Action OnAttackEnd;
        public string ID => data.ID;

        private void Start()
        {
            Initialize();
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
            return Time.time - LastAttackTime < data.AttackCooldown;
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
            visitor.Visit(data.Damage);
        }

        protected virtual void CallOnAttackEnd()
        {
            OnAttackEnd?.Invoke();
        }
    }
}