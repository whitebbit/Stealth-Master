using UnityEngine;

namespace _3._Scripts.Units.Animations.IK
{
    public class IKLookAt : MonoBehaviour
    {
        [SerializeField, Range(0,1)] private float headWeight;
        [SerializeField, Range(0,1)] private float bodyWeight;

        private Transform target;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if(target == null) return;
            
            animator.SetLookAtPosition(target.position + new Vector3(0, 4, 0));
            animator.SetLookAtWeight(1, bodyWeight, headWeight);
        }

        public void SetTarget(Transform obj) => target = obj;
        public void ResetTarget() => target = null;
    }
}