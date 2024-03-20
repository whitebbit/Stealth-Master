using System;
using _3._Scripts.Detectors;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons;
using UnityEngine;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class AttackState : State
    {
        [SerializeField] private Weapon weapon;
        [Space] [SerializeField] private float timeBeforeShot;
        public bool Attacking { get; private set; }
        public IWeaponVisitor LastVisitor { get; set; }

        private float timer;
        private float attackCooldownTimer;
        private bool firstShoot;

        public override void OnEnter()
        {
            base.OnEnter();
            timer = 0;
            attackCooldownTimer = 0;
            firstShoot = true;
        }

        public override void Update()
        {
            TryShoot();

            SetAttackingState();
        }

        private void SetAttackingState()
        {
            if (!Attacking) return;
            attackCooldownTimer += Time.deltaTime;
            if (!(attackCooldownTimer >= weapon.Data.AttackCooldown * 0.75f)) return;

            attackCooldownTimer = 0;
            Attacking = false;
        }

        private void TryShoot()
        {
            if (firstShoot)
            {
                timer += Time.deltaTime;

                if (!(timer >= timeBeforeShot)) return;

                firstShoot = false;
                Shoot();
            }
            else
            {
                Shoot();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            Attacking = false;
            firstShoot = true;
        }

        private void Shoot()
        {
            if (LastVisitor == default && !Attacking) return;

            Attacking = true;
            weapon.Attack(LastVisitor);
        }
    }
}