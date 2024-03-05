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
        [Space]
        [SerializeField] private UnitAnimator unitAnimator;
        [SerializeField] private AimIK aimIK;
        
        private IWeaponVisitor lastVisitor;
        
        private void Start()
        {
            Detector.OnFound += Attack;
            unitAnimator.AnimationEvent += OnAnimationEvent;

            aimIK.solver.transform = transform;
            aimIK.solver.IKPositionWeight = 0;
        }

        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;
            
            LastAttackTime = Time.time;
            lastVisitor = visitor;
            
            aimIK.solver.target = lastVisitor.Transform();
            aimIK.solver.IKPositionWeight = 1;
            
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
            //DoDamage(lastVisitor);
        }
    }
}