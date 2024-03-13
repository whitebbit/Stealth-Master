using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class PatrolState: State
    {
        [SerializeField] private NavMeshAgent agent;
        [Header("Settings")] [SerializeField] private float pauseDuration;
        [Header("Points")] [SerializeField] private List<Transform> points = new();
        
        private float timer;
        private int currentPoint;
        public override void OnEnter()
        {
            base.OnEnter();
            agent.SetDestination(points[0].position);
        }

        public override void Update()
        {
            if (agent.pathPending || !(agent.remainingDistance < 0.1f)) return;
            
            timer += Time.deltaTime;
            agent.isStopped = true;
            
            if (!(timer >= pauseDuration)) return;
            
            currentPoint = (currentPoint + 1) % points.Count;
            agent.isStopped = false;
            agent.SetDestination(points[currentPoint].position);
            timer = 0f; 
        }
    }
}