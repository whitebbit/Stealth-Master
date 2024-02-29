using System;
using _3._Scripts.Gizmos.Enums;
using UnityEngine;

namespace _3._Scripts.Gizmos
{
    [Serializable]
    public class CustomGizmos
    {
        [SerializeField] private DrawGizmosType type;
        [SerializeField] private Color color;

        public DrawGizmosType Type => type;

        public void DrawGizmos(Transform point, Vector3 size, Vector3 offset = default)
        {
            if (type == DrawGizmosType.Never) return;

            var position = point.TransformPoint(offset);
            UnityEngine.Gizmos.color = color;
            UnityEngine.Gizmos.matrix = point.localToWorldMatrix;
            UnityEngine.Gizmos.DrawCube(position, size);
        }
        
        public void DrawGizmos(Transform point, float radius, Vector3 offset = default)
        {
            if (type == DrawGizmosType.Never) return;
            if (point == null) return;

            var position = point.TransformPoint(offset);
            UnityEngine.Gizmos.color = color;
            UnityEngine.Gizmos.matrix = point.localToWorldMatrix;
            UnityEngine.Gizmos.DrawSphere(position, radius);
        }
    }
}