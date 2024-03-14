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

        public override void OnEnter()
        {
            base.OnEnter();
            agent.speed = speed;
            StartPatrol();
        }

        public override void Update()
        {

                    PatrolInPoints();
        }

        public override void OnExit()
        {
            base.OnExit();
            StopMoving();
            ResetTween();
        }

        private void StartPatrol()
        {
            switch (type)
            {
                case PatrolType.None:
                    StopMoving();
                    break;
                case PatrolType.Rotation:
                    if (points.Count > 1)
                        PatrolByPointsRotation();
                    else
                        PatrolBySelfRotation();
                    break;
                case PatrolType.PointToPoint:
                    if (points.Count > 1)
                        StartMoving();
                    else
                        StopMoving();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void PatrolInPoints()
        {
            if (points.Count <= 1 || type != PatrolType.PointToPoint) return;
            if (agent.pathPending || !(agent.remainingDistance < 0.25f)) return;

            StopMoving();
            timer += Time.deltaTime;

            if (!(timer >= pauseDuration)) return;

            currentPoint = (currentPoint + 1) % points.Count;
            StartMoving();
            timer = 0f;
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

        private void ResetTween()
        {
            rotationTween.Pause();
            rotationTween.Kill();
            rotationTween = null;
        }
    }
}