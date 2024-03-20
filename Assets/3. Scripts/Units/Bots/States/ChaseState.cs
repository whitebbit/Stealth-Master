using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class ChaseState : State
    {
        public UnitNavMeshAgent UnitAgent { get; set; }

        [Header("Settings")] [SerializeField] private float speed;
        [SerializeField] private float chasingTime;
        [SerializeField] private float stoppingDistance = 1;

        public IWeaponVisitor LastVisitor { get; set; }
        public event Action OnChasingFinish;
        private float timer;
        private bool goToVisitorLastPosition;

        public override void OnEnter()
        {
            base.OnEnter();
            goToVisitorLastPosition = false;
            timer = 0;
            UnitAgent.Agent.speed = speed;
            UnitAgent.Agent.stoppingDistance = stoppingDistance;
        }

        public override void Update()
        {
            ChaseVisitor();
            MoveToVisitorLastPosition();
            CheckFinishPoint();
        }

        private void MoveToVisitorLastPosition()
        {
            if (!(timer >= chasingTime)) return;
            if (goToVisitorLastPosition) return;

            goToVisitorLastPosition = true;
            UnitAgent.StartMoving(UnitAgent.PointOnNavMesh(LastVisitor.Target().position));
        }

        private void ChaseVisitor()
        {
            if ((timer >= chasingTime)) return;

            timer += Time.deltaTime;
            
            if(LastVisitor != default)
                UnitAgent.StartMoving(LastVisitor.Target().position);
        }

        public override void OnExit()
        {
            base.OnExit();
            UnitAgent.Agent.stoppingDistance = 0;
            UnitAgent.StopMoving();
        }

        private void CheckFinishPoint()
        {
            if (!UnitAgent.OnPoint()) return;
            if (!goToVisitorLastPosition) return;

            UnitAgent.StopMoving();
            OnChasingFinish?.Invoke();
        }
    }
}