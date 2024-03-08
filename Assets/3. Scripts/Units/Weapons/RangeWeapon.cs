using System;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Animations.IK;
using _3._Scripts.Units.Interfaces;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public class RangeWeapon: Weapon
    {
        [Header("IK")]
        [SerializeField] private AimIK aimIK;
        
        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;
            
            LastAttackTime = Time.time;
            
            aimIK.solver.target = visitor.Transform();
            aimIK.solver.IKPositionWeight = 1;
            
            DoAnimation();
        }

        protected override void Initialize()
        {
            Detector.OnFound += Attack;
            unitAnimator.AnimationEvent += OnAnimationEvent;
            unitAnimator.SetController(animatorController);
            
            aimIK.solver.transform = transform;
            aimIK.solver.IKPositionWeight = 0;
        }

        protected override void Resetting()
        {
            Detector.OnFound -= Attack;
            unitAnimator.AnimationEvent -= OnAnimationEvent;
            
            aimIK.solver.transform = null;
            aimIK.solver.IKPositionWeight = 0;
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
        }
    }
}