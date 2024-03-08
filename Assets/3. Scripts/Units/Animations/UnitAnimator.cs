using System;
using UnityEditor.Animations;
using UnityEngine;

namespace _3._Scripts.Units.Animations
{
    public sealed class UnitAnimator : MonoBehaviour
    {
        private Animator animator;
        public event Action<string> AnimationEvent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if (animator == null) animator = GetComponentInChildren<Animator>();
        }

        public void SetTrigger(string key)
        {
            if (animator == null) return;

            animator.SetTrigger(key);
        }


        public void SetFloat(string key, float value)
        {
            if (animator == null) return;

            animator.SetFloat(key, value);
        }

        public void SetController(AnimatorOverrideController controller) =>
            animator.runtimeAnimatorController = controller;

        public void OnEventActivate(string key)
        {
            AnimationEvent?.Invoke(key);
        }
    }
}