using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.Enums;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Units.Bots.States
{
    [Serializable]
    public class PatrolState : State
    {
        [Header("Settings")] [SerializeField] private PatrolType type;
        [SerializeField] private float speed;
        [SerializeField] private float pauseDuration;
        [Header("Points")] [SerializeField] private List<Transform> points = new();

        public UnitNavMeshAgent UnitAgent { get; set; }
        private float timer;
        private int currentPoint;
        private Tween rotationTween;
        public Vector3 StartPosition { get; set; }

        public override void OnEnter()
        {
            base.OnEnter();
            UnitAgent.Agent.speed = speed;
            StartPatrol();
        }

        public override void Update()
        {
            switch (type)
            {
                case PatrolType.None:
                    if (!UnitAgent.OnPoint()) return;
                    UnitAgent.StopMoving();
                    break;
                case PatrolType.Rotation:
                    if (!UnitAgent.OnPoint()) return;
                    UnitAgent.StopMoving();
                    break;
                case PatrolType.PointToPoint:
                    SetNextPoint();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            UnitAgent.StopMoving();
            ResetTween();
            
            UnitAgent.ResetOnStopMoving();;
        }

        private void StartPatrol()
        {
            switch (type)
            {
                case PatrolType.None:
                    UnitAgent.StartMoving(StartPosition);
                    break;
                case PatrolType.Rotation:
                    UnitAgent.StartMoving(StartPosition);
                    UnitAgent.OnStopMoving += StartRotationPatrol;
                    break;
                case PatrolType.PointToPoint:
                    StartPointToPointPatrol();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void StartRotationPatrol()
        {
            if (points.Count > 1)
                PatrolByPointsRotation();
            else
                PatrolBySelfRotation();
        }

        private void StartPointToPointPatrol()
        {
            if (points.Count > 1)
                UnitAgent.StartMoving(points[currentPoint].position);
            else
            {
                type = PatrolType.None;
                UnitAgent.StartMoving(StartPosition);
            }
        }
        
        private void SetNextPoint()
        {
            if (!UnitAgent.OnPoint()) return;

            UnitAgent.StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;

            currentPoint = (currentPoint + 1) % points.Count;
            UnitAgent.StartMoving(points[currentPoint].position);
            timer = 0f;
        }
        
        private void PatrolBySelfRotation()
        {
            var target = UnitAgent.Agent.transform.eulerAngles + new Vector3(0, 180, 0);
            rotationTween = UnitAgent.Agent.transform.DORotate(target, speed, RotateMode.FastBeyond360)
                .SetDelay(pauseDuration)
                .OnComplete(PatrolBySelfRotation);
        }

        private void PatrolByPointsRotation()
        {
            var target = points[currentPoint].position;
            rotationTween = UnitAgent.Agent.transform.DOLookAt(target, speed, AxisConstraint.Y)
                .SetDelay(pauseDuration)
                .OnComplete(PatrolByPointsRotation);

            currentPoint = (currentPoint + 1) % points.Count;
        }
        

        private void ResetTween()
        {
            rotationTween.Pause();
            rotationTween.Kill();
            rotationTween = null;
        }
    }
}