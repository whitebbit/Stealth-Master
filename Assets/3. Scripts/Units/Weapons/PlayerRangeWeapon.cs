using _3._Scripts.Detectors;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Weapons
{
    public class PlayerRangeWeapon : RangeWeapon
    {
        [SerializeField] protected BaseDetector<IWeaponVisitor> detector;

        protected override void Initialize()
        {
            detector.OnFound += Attack;

            unitAnimator.AnimationEvent += OnAnimationEvent;

            unitAnimator.SetController(data.AnimatorController);

            aimIK.solver.transform = transform;
            aimIK.solver.IKPositionWeight = 0;
        }

        protected override void Resetting()
        {
            detector.OnFound -= Attack;

            unitAnimator.AnimationEvent -= OnAnimationEvent;

            aimIK.solver.transform = null;
            aimIK.solver.IKPositionWeight = 0;
        }
    }
}