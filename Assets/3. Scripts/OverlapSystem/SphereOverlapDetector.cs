using _3._Scripts.Gizmos.Enums;
using UnityEngine;

namespace _3._Scripts.OverlapSystem
{
    public class SphereOverlapDetector: OverlapDetector
    {
        [Header("Sphere settings")] 
        [SerializeField] private float radius;
        
        protected override int GetOverlapResult(Vector3 position)
        {
            return Physics.OverlapSphereNonAlloc(position, radius, OverlapResults, searchLayer.value);
        }

        protected override void DrawGizmos()
        {
            gizmos.DrawGizmos(startPoint, radius, offset);
        }
    }
}