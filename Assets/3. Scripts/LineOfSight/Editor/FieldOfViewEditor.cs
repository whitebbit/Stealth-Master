using UnityEditor;
using UnityEngine;

namespace _3._Scripts.LineOfSight.Editor
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var fow = (FieldOfView)target;
            var position = fow.transform.position;
            Handles.color = Color.white;
            Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
            var viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
            var viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

            Handles.DrawLine(position, position + viewAngleA * fow.viewRadius);
            Handles.DrawLine(position, position + viewAngleB * fow.viewRadius);

            Handles.color = Color.red;
            foreach (var visibleTarget in fow.visibleTargets)
            {
                Handles.DrawLine(fow.transform.position, visibleTarget.position);
            }
        }
    }
}