using System;
using _3._Scripts.Detectors.Interfaces;
using _3._Scripts.Gizmos;
using _3._Scripts.Gizmos.Enums;
using UnityEngine;

namespace _3._Scripts.Detectors.OverlapSystem.Base
{
    public abstract class OverlapDetector<T> : BaseDetector<T>
    {
        [Header("Mask")] [SerializeField] protected LayerMask searchLayer;
        [SerializeField] protected LayerMask obstacleLayer;

        [Header("Overlap area")] [SerializeField]
        protected Transform startPoint;

        [SerializeField] protected Vector3 offset;
        [Header("Obstacle")] [SerializeField] protected bool considerObstacles;
        
        [Header("Gizmos")] [SerializeField] protected CustomGizmos gizmos;
        
        protected readonly Collider[] OverlapResults = new Collider[32];
        private int overlapResultsCount;


        private void Update()
        {
            if (ObjectsDetected())
            {
                InteractWithFoundedObjects();
            }
        }
        
        private void InteractWithFoundedObjects()
        {
            for (var i = 0; i < overlapResultsCount; i++)
            {
                if(OverlapResults[i].TryGetComponent(out T findable) == false) continue;

                if (considerObstacles)
                {
                    var startPosition = startPoint.position + offset;
                    var colliderPosition = OverlapResults[i].transform.position;
                    var hasObstacle = Physics.Linecast(startPosition, colliderPosition, obstacleLayer);
                    if(hasObstacle) continue;
                }
                
                CallOnFound(findable);
            }
        }
        public override bool ObjectsDetected()
        {
            var position = startPoint.TransformPoint(offset);
            overlapResultsCount = GetOverlapResult(position);
            
            return overlapResultsCount > 0;
        }
        
        protected abstract int GetOverlapResult(Vector3 position);
        protected abstract void DrawGizmos();
        
        private void OnDrawGizmos()
        {
            if (gizmos.Type == DrawGizmosType.Always)
                DrawGizmos();
        }
        private void OnDrawGizmosSelected()
        {
            if (gizmos.Type  == DrawGizmosType.OnSelected)
                DrawGizmos();
        }
    }
}