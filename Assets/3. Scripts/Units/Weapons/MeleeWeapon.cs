using System;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using UnityEngine;


namespace _3._Scripts.Units.Weapons
{
    public class MeleeWeapon : Weapon
    {      
        private IWeaponVisitor lastVisitor;
        
        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;
            
            LastAttackTime = Time.time;
            lastVisitor = visitor;
            DoAnimation();
        }

        protected override void Initialize()
        {
            Detector.OnFound += Attack;
            unitAnimator.AnimationEvent += OnAnimationEvent;
            unitAnimator.SetController(animatorController);
        }

        protected override void Resetting()
        {
            Detector.OnFound -= Attack;
            unitAnimator.AnimationEvent -= OnAnimationEvent;
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