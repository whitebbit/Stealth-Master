using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class PatrolState : State
    {
        [Header("Components")] [SerializeField]
        private NavMeshAgent agent;

        [SerializeField] private UnitAnimator animator;
        [Header("Settings")] [SerializeField] private float speed;
        [SerializeField] private float pauseDuration;
        [Header("Points")] [SerializeField] private List<Transform> points = new();

        private float timer;
        private int currentPoint;

        public override void OnEnter()
        {
            base.OnEnter();
            agent.speed = speed;
            StartMoving();
        }

        public override void Update()
        {
            PatrolInPoints();
        }

        private void PatrolInPoints()
        {
            if (agent.pathPending || !(agent.remainingDistance < 0.25f)) return;

            StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;

            currentPoint = (currentPoint + 1) % points.Count;
            StartMoving();
            timer = 0f;
        }

        private void StartMoving()
        {
            agent.isStopped = false;
            animator.SetFloat("Speed", 1);
            agent.SetDestination(points[currentPoint].position);
        }

        private void StopMoving()
        {
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }
    }
}