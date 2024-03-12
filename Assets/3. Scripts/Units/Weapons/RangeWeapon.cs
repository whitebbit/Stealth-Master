using System;
using System.Collections;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Animations.IK;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons.Scriptable;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _3._Scripts.Units.Weapons
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private AimIK aimIK;

        [Header("Range Weapon")] [SerializeField]
        private RangeWeaponData rangeWeaponData;

        [SerializeField] private ParticleSystem muzzleEffect;

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

            unitAnimator.SetController(data.AnimatorController);

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
            StopAllCoroutines();
            StartCoroutine(DelayedPerformAttack());
        }

        private void OnAnimationEvent(string key)
        {
            switch (key)
            {
                case "RangeAttack":
                    PerformAttack();
                    PlaySound();
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
            var shoulder = unitAnimator.GetBoneTransform(HumanBodyBones.RightShoulder);
            var position = shoulder.position;
            var direction = lastVisitor.Transform().position - position;
            var spreadFactor = rangeWeaponData.SpreadFactor;
            
            for (var i = 0; i < rangeWeaponData.BulletCount; i++)
            {
                var spread = new Vector3(Random.Range(-spreadFactor, spreadFactor),
                    Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor));
                var b = Instantiate(rangeWeaponData.Bullet, position, Quaternion.LookRotation(direction + spread));

                CreateParticle();

                b.SetDamage(data.Damage);
                b.AddForce(direction + spread, 15);
                yield return new WaitForSeconds(rangeWeaponData.AttackSpeed);
            }
        }
    }
}