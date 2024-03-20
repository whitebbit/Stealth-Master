using System;
using _3._Scripts.Detectors;
using _3._Scripts.Interactive.Interfaces;
using UnityEngine;

namespace _3._Scripts.Interactive
{
    public class Interactor: MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent(out IInteractive interactive))
                interactive.Interact();
        }
    }
}