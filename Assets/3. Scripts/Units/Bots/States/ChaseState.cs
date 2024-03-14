using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Bots.Enums;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class ChaseState: State
    {
        [Header("Components")] [SerializeField]
        private NavMeshAgent agent;
        [SerializeField] private UnitAnimator animator;
        [Header("Settings")]
        [SerializeField] private float speed;
        [SerializeField] private float stoppingDistance = 1;
        
        public Vector3 VisitorLastPosition { get; set; }
        public event Action onChasingFinish;
        public override void OnEnter()
        {
            base.OnEnter();
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
            StartMoving();
        }

        public override void Update()
        {
            CheckFinishPoint();
        }

        public override void OnExit()
        {
            base.OnExit();
            agent.stoppingDistance = 0;
            StopMoving();
        }
        private void StartMoving()
        {
            agent.isStopped = false;
            animator.SetFloat("Speed", 1);
            agent.SetDestination(VisitorLastPosition);
        }
        
        private void StopMoving()
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }
        
        private void CheckFinishPoint()
        {
            if (agent.pathPending || !(agent.remainingDistance < stoppingDistance)) return;

            StopMoving();
            onChasingFinish?.Invoke();
        }
        
        
    }
}