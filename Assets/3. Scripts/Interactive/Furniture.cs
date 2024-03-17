using System;
using System.Collections;
using System.Collections.Generic;
using _3._Scripts.Interactive.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _3._Scripts.Interactive
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshObstacle))]
    public class Furniture : MonoBehaviour, IInteractive
    {
        [SerializeField] private float brokenForce = 125;
        [Space] [SerializeField] private Transform clear;
        [SerializeField] private Transform broken;

        private readonly List<Collider> brokenColliders = new();
        private readonly List<Rigidbody> brokenRigidbodies = new();
        private Collider coll;

        private void Awake()
        {
            coll = GetComponent<Collider>();
            foreach (Transform b in broken)
            {
                if(b.TryGetComponent(out Collider c))
                    brokenColliders.Add(c);
                if(b.TryGetComponent(out Rigidbody rb))
                    brokenRigidbodies.Add(rb);
            }
        }

        public void Interact()
        {
            clear.gameObject.SetActive(false);
            broken.gameObject.SetActive(true);

            coll.enabled = false;

            foreach (Transform b in broken)
            {
                b.gameObject.layer = LayerMask.NameToLayer("Ignore");
            }

            foreach (var b in brokenRigidbodies)
            {
                var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                b.AddForce(direction * brokenForce, ForceMode.Force);
            }

            StartCoroutine(DelayDisable());
        }

        private IEnumerator DelayDisable()
        {
            yield return new WaitForSeconds(2);
            foreach (var b in brokenColliders)
            {
                b.enabled = false;
            }

            Destroy(gameObject, 1);
        }
    }
}