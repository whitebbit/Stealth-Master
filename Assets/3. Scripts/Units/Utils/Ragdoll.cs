using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.Units.Utils
{
    public class Ragdoll : MonoBehaviour
    {
        public event Action<bool> onStateChanged; 
        private bool state;
        
        private List<Rigidbody> rigidbodies = new();
        private List<Collider> colliders = new();

        private Rigidbody mainRigidbody;
        private Collider mainCollider;
        
        public bool State
        {
            set
            {
                state = value;
                CollidersState(state);
                RigidbodiesState(state);
                
                onStateChanged?.Invoke(state);
            }
        }

        private void Awake()
        {
            InitializeRigidbodies();
            InitializeColliders();
        }

#if UNITY_EDITOR

        private void Update()
        {
            //State = state;
        }

#endif
        private void Start()
        {
            State = false;
        }

        private void InitializeColliders()
        {
            colliders = new List<Collider>(GetComponentsInChildren<Collider>());
            mainCollider = GetComponent<Collider>();
            
            if (mainCollider != null)
                colliders.Remove(mainCollider);
        }

        private void InitializeRigidbodies()
        {
            rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            mainRigidbody = GetComponent<Rigidbody>();
            if (mainRigidbody != null)
                rigidbodies.Remove(mainRigidbody);
        }


        private void CollidersState(bool state)
        {
            if (mainCollider != null)
                mainCollider.enabled = !state;
            
            foreach (var collider in colliders)
            {
                collider.enabled = state;
            }
        }

        private void RigidbodiesState(bool state)
        {
            if (mainRigidbody != null)
                mainRigidbody.isKinematic = state;
            
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = !state;
            }
        }
    }
}