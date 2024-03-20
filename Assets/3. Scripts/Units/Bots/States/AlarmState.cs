using System;
using _3._Scripts.FSM.Base;
using UnityEngine;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class AlarmState: State
    {
        public UnitNavMeshAgent UnitAgent { get; set; }
        [Header("Settings")] [SerializeField] private float speed;
        [SerializeField] private float pauseDuration;
        [SerializeField] private float stoppingDistance;
        [Header("Points")] 
        [SerializeField] private float minSearchRange;
        [SerializeField] private float maxSearchRange;
        private float timer;

        public override void OnEnter()
        {
            base.OnEnter();
            UnitAgent.Agent.speed = speed;
            UnitAgent.Agent.stoppingDistance = stoppingDistance;
            UnitAgent.StartMoving(UnitAgent.RandomPointOnNavMesh(minSearchRange, maxSearchRange));
        }

        public override void Update()
        {
            SetNextPoint();
        }

        public override void OnExit()
        {
            base.OnExit();
            UnitAgent.Agent.stoppingDistance = 0;
            UnitAgent.StopMoving();
        }
        
        private void SetNextPoint()
        {
            if (!UnitAgent.OnPoint()) return;

            UnitAgent.StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;
            
            UnitAgent.StartMoving(UnitAgent.RandomPointOnNavMesh(minSearchRange, maxSearchRange));
            timer = 0f;
        }
    }
}