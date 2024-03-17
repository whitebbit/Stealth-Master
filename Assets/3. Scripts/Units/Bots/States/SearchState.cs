using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using UnityEngine;
using UnityEngine.AI;
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
        [SerializeField] private float minSearchRange;
        [SerializeField] private float maxSearchRange;

        public event Action OnSearchEnd;
        public bool GoToSearchPoint { get; set; }
        public Vector3 SearchPoint { get; set; }

        private List<Vector3> points = new();
        private float timer;

        private int currentPoint;

        public override void OnEnter()
        {
            base.OnEnter();
            UnitAgent.Agent.speed = speed;
            UnitAgent.Agent.stoppingDistance = stoppingDistance;

            if (!GoToSearchPoint)
            {
                InitializePoints();
                UnitAgent.StartMoving(points[currentPoint]);
            }
            else
            {
                points.Add(UnitAgent.PointOnNavMesh(SearchPoint, maxSearchRange));
                UnitAgent.StartMoving(points[currentPoint]);
            }
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
            GoToSearchPoint = false;
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
                points.Add(UnitAgent.RandomPointOnNavMesh(minSearchRange, maxSearchRange));
            }
        }

        
    }
}