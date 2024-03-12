using System;
using _3._Scripts.Detectors;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Interfaces;
using DG.Tweening;
using UnityEngine;


namespace _3._Scripts.Units.Weapons.Utils
{
    public class WeaponAttackMover: MonoBehaviour
    {
        [SerializeField] private Vector3 onAttackPosition;
        [SerializeField] private Vector3 onAttackRotation;
        [SerializeField] private UnitAnimator unitAnimator;
        
        private Vector3 startRotation;
        private Vector3 startPosition;
        private void Awake()
        {

            var t = transform;
            startRotation = t.localEulerAngles;
            startPosition = t.localPosition;
        }

        private void Start()
        {
            unitAnimator.AnimationEvent += ChangeState;
        }

        private void ChangeState(string obj)
        {
            if(obj != "AnimationEnd") return;
            switch (obj)
            {
                case "AnimationEnd":
                    transform.DOLocalMove(startPosition, 0.1f);
                    transform.DOLocalRotate(startRotation, 0.1f);
                    break;
                case "RangeAttack":
                    transform.DOLocalMove(onAttackPosition, 0.1f);
                    transform.DOLocalRotate(onAttackRotation, 0.1f);
                    break;
            }
        }
    }
}