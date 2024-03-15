using System;
using _3._Scripts.Units.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Units
{
    [Serializable]
    public class UnitNavMeshAgent
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private UnitAnimator animator;

        public NavMeshAgent Agent => agent;
        public event Action OnStopMoving;

        public void StartMoving(Vector3 position)
        {
            agent.isStopped = false;
            animator.SetFloat("Speed", 1);
            agent.SetDestination(position);
        }

        public void StopMoving()
        {
            if(!agent.isStopped)
                OnStopMoving?.Invoke();
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }

        public bool OnPoint()
        {
            return !agent.pathPending && agent.remainingDistance < 0.25f;
        }

        public void ResetOnStopMoving() => OnStopMoving = null;
    }
}