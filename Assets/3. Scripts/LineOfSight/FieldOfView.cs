using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.LineOfSight
{
    public class FieldOfView : MonoBehaviour
    {
        public float viewRadius;
        [Range(0, 360)] public float viewAngle;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        [HideInInspector] public List<Transform> visibleTargets = new List<Transform>();

        public float meshResolution;
        public int edgeResolveIterations;
        public float edgeDstThreshold;

        public MeshFilter viewMeshFilter;
        private Mesh viewMesh;

        private void Start()
        {
            viewMesh = new Mesh
            {
                name = "View Mesh"
            };
            viewMeshFilter.mesh = viewMesh;

            StartCoroutine(nameof(FindTargetsWithDelay), .2f);
        }


        private IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void LateUpdate()
        {
            DrawFieldOfView();
        }

        private void FindVisibleTargets()
        {
            visibleTargets.Clear();
            var targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            foreach (var t in targetsInViewRadius)
            {
                var target = t.transform;
                var dirToTarget = (target.position - transform.position).normalized;
                if (!(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)) continue;
                var dstToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        private void DrawFieldOfView()
        {
            var stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
            var stepAngleSize = viewAngle / stepCount;
            var viewPoints = new List<Vector3>();
            var oldViewCast = new ViewCastInfo();
            for (var i = 0; i <= stepCount; i++)
            {
                var angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
                var newViewCast = ViewCast(angle);

                if (i > 0)
                {
                    var edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
                    if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && edgeDstThresholdExceeded))
                    {
                        var edge = FindEdge(oldViewCast, newViewCast);
                        if (edge.PointA != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointA);
                        }

                        if (edge.PointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.PointB);
                        }
                    }
                }


                viewPoints.Add(newViewCast.Point);
                oldViewCast = newViewCast;
            }

            var vertexCount = viewPoints.Count + 1;
            var vertices = new Vector3[vertexCount];
            var triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector3.zero;
            for (var i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

                if (i >= vertexCount - 2) continue;
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

            viewMesh.Clear();

            viewMesh.vertices = vertices;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();
        }


        private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            var minAngle = minViewCast.Angle;
            var maxAngle = maxViewCast.Angle;
            var minPoint = Vector3.zero;
            var maxPoint = Vector3.zero;

            for (var i = 0; i < edgeResolveIterations; i++)
            {
                var angle = (minAngle + maxAngle) / 2;
                var newViewCast = ViewCast(angle);

                var edgeDstThresholdExceeded = Mathf.Abs(minViewCast.Dst - newViewCast.Dst) > edgeDstThreshold;
                if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.Point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.Point;
                }
            }

            return new EdgeInfo(minPoint, maxPoint);
        }


        private ViewCastInfo ViewCast(float globalAngle)
        {
            var dir = DirFromAngle(globalAngle, true);

            return Physics.Raycast(transform.position, dir, out var hit, viewRadius, obstacleMask)
                ? new ViewCastInfo(true, hit.point, hit.distance, globalAngle)
                : new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        private struct ViewCastInfo
        {
            public readonly bool Hit;
            public readonly Vector3 Point;
            public readonly float Dst;
            public readonly float Angle;

            public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
            {
                Hit = hit;
                Point = point;
                Dst = dst;
                Angle = angle;
            }
        }

        private struct EdgeInfo
        {
            public readonly Vector3 PointA;
            public readonly Vector3 PointB;

            public EdgeInfo(Vector3 pointA, Vector3 pointB)
            {
                PointA = pointA;
                PointB = pointB;
            }
        }
    }
}