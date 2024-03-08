using System;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Animations.IK;
using _3._Scripts.Units.Interfaces;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _3._Scripts.Units.Weapons
{
    public class RangeWeapon : Weapon
    {
        [Header("Range Weapon Settings")] [SerializeField]
        private int bulletCount;

        [SerializeField] private float spreadFactor;
        [Space] [SerializeField] private Transform shotPoint;
        [SerializeField] private Bullet bullet;

        [Header("IK")] [SerializeField] private AimIK aimIK;


        private IWeaponVisitor lastVisitor;

        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;

            LastAttackTime = Time.time;

            aimIK.solver.target = visitor.Transform();
            aimIK.solver.IKPositionWeight = 1;
            lastVisitor = visitor;

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

        protected override void PerformAttack()
        {
            for (var i = 0; i < bulletCount; i++)
            {
                var position = shotPoint.position;
                var b = Instantiate(bullet, position, shotPoint.rotation);
                var direction = lastVisitor.Transform().position - position;
                var spread = new Vector3(Random.Range(-spreadFactor, spreadFactor),
                    Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor));
                
                b.SetDamage(damage);
                b.AddForce(direction + spread, 10);
            }
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