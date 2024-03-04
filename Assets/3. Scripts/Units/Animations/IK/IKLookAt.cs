using UnityEngine;

namespace _3._Scripts.Units.Animations.IK
{
    public class IKLookAt : MonoBehaviour
    {
        [SerializeField] private float headWeight;
        [SerializeField] private float bodyWeight;

        [SerializeField] private Transform target;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if(target == null) return;
            
            animator.SetLookAtPosition(target.position);
            animator.SetLookAtWeight(1, bodyWeight, headWeight);
        }

        public void SetTarget(Transform obj) => target = obj;
        public void ResetTarget() => target = null;
    }
}