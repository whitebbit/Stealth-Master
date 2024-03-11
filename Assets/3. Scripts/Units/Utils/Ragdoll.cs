using System;
using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.Units.Utils
{
    public class Ragdoll : MonoBehaviour
    {
        [SerializeField] private bool state;

        private List<Rigidbody> rigidbodies = new();
        private List<Collider> colliders = new();

        public bool State
        {
            get => state;
            set
            {
                state = value;
                CollidersState(state);
                RigidbodiesState(state);
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
            if (State != state)
                State = state;
        }

#endif
        private void Start()
        {
            State = false;
        }

        private void InitializeColliders()
        {
            colliders = new List<Collider>(GetComponentsInChildren<Collider>());
            var coll = GetComponent<Collider>();
            if (coll != null)
                colliders.Remove(coll);
        }

        private void InitializeRigidbodies()
        {
            rigidbodies = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            var rb = GetComponent<Rigidbody>();
            if (rb != null)
                rigidbodies.Remove(rb);
        }


        private void CollidersState(bool state)
        {
            foreach (var collider in colliders)
            {
                collider.enabled = state;
            }
        }

        private void RigidbodiesState(bool state)
        {
            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = !state;
            }
        }
    }
}