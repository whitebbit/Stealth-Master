using System;
using System.Collections.Generic;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Bots.Enums;
using DG.Tweening;
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
        [Header("Settings")] [SerializeField] private PatrolType type;
        [SerializeField] private float speed;
        [SerializeField] private float pauseDuration;
        [Header("Points")] [SerializeField] private List<Transform> points = new();

        private float timer;
        private int currentPoint;
        private Tween rotationTween;
        public Vector3 StartPosition { get; set; }
        private event Action OnFinishPoint;

        public override void OnEnter()
        {
            base.OnEnter();
            agent.speed = speed;
            StartPatrol();
        }

        public override void Update()
        {
            switch (type)
            {
                case PatrolType.None:
                    if (!OnPoint()) return;
                    StopMoving();
                    break;
                case PatrolType.Rotation:
                    if (!OnPoint()) return;
                    StopMoving();
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
            StopMoving();
            ResetTween();
            
            OnFinishPoint = null;
        }

        private void StartPatrol()
        {
            switch (type)
            {
                case PatrolType.None:
                    StartMoving(StartPosition);
                    break;
                case PatrolType.Rotation:
                    StartMoving(StartPosition);
                    OnFinishPoint += StartRotationPatrol;
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
                StartMoving(points[currentPoint].position);
            else
            {
                type = PatrolType.None;
                StartMoving(StartPosition);
            }
        }
        
        private void SetNextPoint()
        {
            if (!OnPoint()) return;

            StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;

            currentPoint = (currentPoint + 1) % points.Count;
            StartMoving(points[currentPoint].position);
            timer = 0f;
        }

        private bool OnPoint()
        {
            return !agent.pathPending && agent.remainingDistance < 0.25f;
        }

        private void PatrolBySelfRotation()
        {
            var target = agent.transform.eulerAngles + new Vector3(0, 180, 0);
            rotationTween = agent.transform.DORotate(target, speed, RotateMode.FastBeyond360)
                .SetDelay(pauseDuration)
                .OnComplete(PatrolBySelfRotation);
        }

        private void PatrolByPointsRotation()
        {
            var target = points[currentPoint].position;
            rotationTween = agent.transform.DOLookAt(target, speed, AxisConstraint.Y)
                .SetDelay(pauseDuration)
                .OnComplete(PatrolByPointsRotation);

            currentPoint = (currentPoint + 1) % points.Count;
        }

        private void StartMoving(Vector3 position)
        {
            agent.isStopped = false;
            animator.SetFloat("Speed", 1);
            agent.SetDestination(position);
        }

        private void StopMoving()
        {
            if(!agent.isStopped)
                OnFinishPoint?.Invoke();
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
        }

        private void ResetTween()
        {
            rotationTween.Pause();
            rotationTween.Kill();
            rotationTween = null;
        }
    }
}