using System;
using _3._Scripts.Detectors;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Weapons;
using UnityEngine;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class AttackState: State
    {
        [SerializeField] private Weapon weapon;
    
        
        public bool Attacking { get; private set; }
        public IWeaponVisitor LastVisitor { get; set; }
        public override void OnEnter()
        {
            base.OnEnter();
            Attacking = true;
            weapon.OnAttackEnd += ChangeAttackState;
        }

        public override void Update()
        {
            if(LastVisitor == default) return;
            
            weapon.Attack(LastVisitor);
        }

        public override void OnExit()
        {
            base.OnExit();
            weapon.OnAttackEnd -= ChangeAttackState;
        }

        private void ChangeAttackState()
        {
            Attacking = false;
        }
    }
}