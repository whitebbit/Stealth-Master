using System;
using _3._Scripts.Gizmos;
using _3._Scripts.Gizmos.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.OverlapSystem
{
    public abstract class OverlapDetector : MonoBehaviour
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


        private bool TryDetect()
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