using System;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Utils;
using DG.Tweening;
using RootMotion.FinalIK;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class Player : Unit
    {
        private UnitAnimator unitAnimator;
        private AimIK aimIK;
        private PlayerMovement playerMovement;
        private Rigidbody rb;
        public override void Dead()
        {
            
        }

        public Tween GoToFinishPoint(Transform point, float duration,Action onComplete = null)
        {
            playerMovement.enabled = false;
            var position = point.position;
            transform.DOLookAt(position, 0.25f, AxisConstraint.Y);
            var t = transform.DOMove(position, duration).OnComplete(() =>
            {
                unitAnimator.SetFloat("Speed", 0);
                onComplete?.Invoke();
            }).OnStart(() =>
            {
                unitAnimator.SetFloat("Speed", 1f);
                rb.isKinematic = true;
            })
            .SetEase(Ease.Linear);

            return t;
        }
        
        protected override void OnAwake()
        {
            var ragdoll = GetComponent<Ragdoll>();
            unitAnimator = GetComponent<UnitAnimator>();
            aimIK = GetComponent<AimIK>();
            playerMovement = GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody>();
            ragdoll.OnStateChanged += ChangeStateByRagdoll;
        }
        
        private void ChangeStateByRagdoll(bool state)
        {
            unitAnimator.SetState(!state);
            aimIK.enabled = !state;
            playerMovement.enabled = !state;
        }
    }
}