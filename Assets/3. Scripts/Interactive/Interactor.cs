using System;
using _3._Scripts.Detectors;
using _3._Scripts.Interactive.Interfaces;
using UnityEngine;

namespace _3._Scripts.Interactive
{
    public class Interactor: MonoBehaviour
    {
        private BaseDetector<IInteractive> detector;

        private void Awake()
        {
            detector = GetComponent<BaseDetector<IInteractive>>();
        }

        private void OnEnable()
        {
            detector.OnFound += DetectorOnFound;
        }

        private void DetectorOnFound(IInteractive obj)
        {
            obj.Interact();
        }

        private void OnDisable()
        {
            detector.OnFound -= DetectorOnFound;
        }
    }
}