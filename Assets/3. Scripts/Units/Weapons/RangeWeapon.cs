using System;
using System.Collections;
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
        [SerializeField] private float attackSpeed;
        [SerializeField] private Bullet bullet;
        [SerializeField] private ParticleSystem muzzleEffect;

        [Header("IK")] [SerializeField] private AimIK aimIK;
        private float aimIKWeight;

        private IWeaponVisitor lastVisitor;

        private void Update()
        {
            ChangeAimWeight();
        }

        public override void Attack(IWeaponVisitor visitor)
        {
            if (CanAttack()) return;

            LastAttackTime = Time.time;
            lastVisitor = visitor;

            aimIK.solver.target = visitor.Transform();
            aimIKWeight = 1;

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

        protected override void CreateParticle()
        {
            muzzleEffect.Play();
        }

        protected override void PerformAttack()
        {
            StartCoroutine(DelayedPerformAttack());
        }

        private void OnAnimationEvent(string key)
        {
            switch (key)
            {
                case "RangeAttack":
                    PerformAttack();
                    PlaySound();
                    CreateParticle();
                    break;
                case "AnimationEnd":
                    aimIKWeight = 0;
                    break;
            }
        }

        private void ChangeAimWeight()
        {
            aimIK.solver.IKPositionWeight = Mathf.Lerp(aimIK.solver.IKPositionWeight, aimIKWeight, Time.deltaTime * 10);
        }

        private IEnumerator DelayedPerformAttack()
        {
            for (var i = 0; i < bulletCount; i++)
            {
                var position = unitAnimator.GetBoneTransform(HumanBodyBones.RightShoulder).position;
                var spread = new Vector3(Random.Range(-spreadFactor, spreadFactor),
                    Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor));
                var direction = lastVisitor.Transform().position - position + spread;
                var b = Instantiate(bullet, position, Quaternion.LookRotation(direction));

                b.SetDamage(damage);
                b.AddForce(direction, 10);
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }
}