using UnityEngine;

namespace _3._Scripts.OverlapSystem
{
    public class BoxOverlapDetector : OverlapDetector
    {
        [Header("Box settings")] [SerializeField]
        private Vector3 size;
        protected override int GetOverlapResult(Vector3 position)
        {
            return Physics.OverlapBoxNonAlloc(position, size / 2, OverlapResults, startPoint.rotation, searchLayer.value);
        }

        protected override void DrawGizmos()
        {
            gizmos.DrawGizmos(startPoint, size, offset);
        }
    }
}