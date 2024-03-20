using System.Collections;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons.Scriptable;
using RootMotion.FinalIK;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Units.Weapons
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] protected AimIK aimIK;
        [SerializeField] protected HumanBodyBones shootBones = HumanBodyBones.RightShoulder;

        [Header("Range Weapon")] [SerializeField]
        private RangeWeaponData rangeWeaponData;

        [SerializeField] private ParticleSystem muzzleEffect;

        private float aimIKWeight;

        private IWeaponVisitor lastVisitor;
        private bool attacking;

        private void Update()
        {
            ChangeAimWeight();
        }

        public override void Attack(IWeaponVisitor visitor)
        {
            if (visitor == default) return;
            if (attacking) return;
            if (!CanAttack()) return;

            LastAttackTime = Time.time;
            lastVisitor = visitor;

            aimIK.solver.target = visitor.Target();
            aimIKWeight = 1;
            DoAnimation();
        }

        protected override void Initialize()
        {
            unitAnimator.AnimationEvent += OnAnimationEvent;

            unitAnimator.SetController(data.AnimatorController);

            aimIK.solver.transform = transform;
            aimIK.solver.IKPositionWeight = 0;
        }

        protected override void Resetting()
        {
            unitAnimator.AnimationEvent -= OnAnimationEvent;

            aimIK.solver.transform = null;
            aimIK.solver.IKPositionWeight = 0;
        }

        protected override void DoAnimation()
        {
            unitAnimator.SetTrigger("Attack");
            unitAnimator.SetBool("Attacking", true);
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

        protected void OnAnimationEvent(string key)
        {
            if (attacking) return;
            switch (key)
            {
                case "RangeAttack":
                    attacking = true;
                    PerformAttack();
                    PlaySound();
                    break;
            }
        }

        private void ChangeAimWeight()
        {
            aimIK.solver.IKPositionWeight = Mathf.Lerp(aimIK.solver.IKPositionWeight, aimIKWeight, Time.deltaTime * 10);
        }

        private IEnumerator DelayedPerformAttack()
        {
            var bones = unitAnimator.GetBoneTransform(shootBones);
            var position = bones.position;
            var spreadFactor = rangeWeaponData.SpreadFactor;

            for (var i = 0; i < rangeWeaponData.BulletCount; i++)
            {
                var spread = new Vector3(Random.Range(-spreadFactor, spreadFactor),
                    Random.Range(-spreadFactor, spreadFactor), Random.Range(-spreadFactor, spreadFactor));
                var direction = lastVisitor.Target().position - position;
                var b = Instantiate(rangeWeaponData.Bullet, position, Quaternion.LookRotation(direction + spread));

                CreateParticle();

                b.SetDamage(data.Damage);
                b.AddForce(direction + spread, rangeWeaponData.Force);
                yield return new WaitForSeconds(rangeWeaponData.AttackSpeed);
            }

            unitAnimator.SetBool("Attacking", false);
            
            yield return new WaitForSeconds(rangeWeaponData.AttackSpeed);
            aimIKWeight = 0;

            yield return new WaitForSeconds(data.AttackCooldown - (Time.time - LastAttackTime));
            attacking = false;
            CallOnAttackEnd();
        }
    }
}