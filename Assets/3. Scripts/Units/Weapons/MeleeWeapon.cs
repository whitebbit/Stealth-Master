using System;
using _3._Scripts.OverlapSystem;
using _3._Scripts.OverlapSystem.Base;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public class MeleeWeapon : Weapon
    {
        public OverlapDetector<IWeaponVisitor> overlapDetector;
        public UnitAnimator unitAnimator;

        private IWeaponVisitor lastVisitor;
        private void Start()
        {
            overlapDetector.OnFound += Attack;
            unitAnimator.AnimationEvent += OnAnimationEvent;
        }

        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;
            
            LastAttackTime = Time.time;
            lastVisitor = visitor;
            DoAnimation();
        }

        protected override void DoAnimation()
        {
            unitAnimator.SetTrigger("Attack");
        }

        private void OnAnimationEvent(string key)
        {
            if (key != "MeleeAttack") return;
            
            PlaySound();
            PerformAttack();
            CreateParticle();
            DoDamage(lastVisitor);
        }
    }
}