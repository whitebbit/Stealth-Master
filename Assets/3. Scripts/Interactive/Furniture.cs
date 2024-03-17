using System;
using System.Collections;
using System.Collections.Generic;
using _3._Scripts.Interactive.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Interactive
{
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
                brokenColliders.Add(b.GetComponent<Collider>());
                brokenRigidbodies.Add(b.GetComponent<Rigidbody>());
            }
        }

        public void Interact()
        {
            clear.gameObject.SetActive(false);
            broken.gameObject.SetActive(true);

            coll.enabled = false;

            foreach (var b in brokenColliders)
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