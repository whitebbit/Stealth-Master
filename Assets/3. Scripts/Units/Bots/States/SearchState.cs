using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class SearchState : State
    {
        public UnitNavMeshAgent UnitAgent { get; set; }

        [Header("Settings")] [SerializeField] private float speed;
        [SerializeField] private float pauseDuration;
        [SerializeField] private float stoppingDistance;
        [Header("Points")] [SerializeField] private int pointsCount;
        [SerializeField] private float searchRange;

        public event Action OnSearchEnd;
        private List<Vector3> points = new();
        private float timer;

        private int currentPoint;

        public override void OnEnter()
        {
            base.OnEnter();
            UnitAgent.Agent.speed = speed;
            UnitAgent.Agent.stoppingDistance = stoppingDistance;

            InitializePoints();
            UnitAgent.StartMoving(points[currentPoint]);
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
            currentPoint = 0;
            OnSearchEnd?.Invoke();
        }

        private void SetNextPoint()
        {
            if (!UnitAgent.OnPoint()) return;

            UnitAgent.StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;

            currentPoint = (currentPoint + 1) % points.Count;

            if (currentPoint == 0)
            {
                UnitAgent.StopMoving();
                OnSearchEnd?.Invoke();
            }

            UnitAgent.StartMoving(points[currentPoint]);
            timer = 0f;
        }

        private void InitializePoints()
        {
            points.Clear();
            for (var i = 0; i < pointsCount; i++)
            {
                points.Add(UnitAgent.Agent.transform.position + new Vector3(Random.Range(-searchRange, searchRange),
                    Random.Range(-searchRange, searchRange), Random.Range(-searchRange, searchRange)));
            }
        }
    }
}