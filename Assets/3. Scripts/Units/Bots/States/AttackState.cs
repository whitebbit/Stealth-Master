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
        private bool firstShoot;

        public override void OnEnter()
        {
            base.OnEnter();
            timer = 0;
            firstShoot = true;
            weapon.OnAttackEnd += ChangeAttackState;
        }

        public override void Update()
        {
            TryShoot();
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
            weapon.OnAttackEnd -= ChangeAttackState;
        }

        private void Shoot()
        {
            if (LastVisitor == default && !Attacking)
            {
                ChangeAttackState();
                return;
            }

            Attacking = true;
            weapon.Attack(LastVisitor);
        }

        private void ChangeAttackState()
        {
            Attacking = false;
        }
    }
}