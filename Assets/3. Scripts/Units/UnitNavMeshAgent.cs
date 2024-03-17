using System;
using _3._Scripts.Units.Animations;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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
            if (!agent.isStopped)
                OnStopMoving?.Invoke();
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }

        public bool OnPoint()
        {
            return !agent.pathPending && agent.remainingDistance < 0.25f;
        }

        public void ResetOnStopMoving() => OnStopMoving = null;

        public Vector3 PointOnNavMesh(Vector3 position, float maxDistance = 5)
        {
            return !NavMesh.SamplePosition(position, out var hit, maxDistance, NavMesh.AllAreas)
                ? position
                : hit.position;
        }
        public Vector3 RandomPointOnNavMesh(float min, float max)
        {
            var randomDirection = Random.insideUnitSphere * Random.Range(min, max);
            randomDirection += Agent.transform.position;

            return PointOnNavMesh(randomDirection, max);
        }
    }
}