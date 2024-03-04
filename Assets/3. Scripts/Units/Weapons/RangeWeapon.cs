using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Weapons
{
    public class RangeWeapon: Weapon
    {
        public UnitAnimator unitAnimator;

        private IWeaponVisitor lastVisitor;
        private void Start()
        {
            detector.OnFound += Attack;
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
            if (key != "RangeAttack") return;
            
            PlaySound();
            PerformAttack();
            CreateParticle();
            DoDamage(lastVisitor);
        }
    }
}