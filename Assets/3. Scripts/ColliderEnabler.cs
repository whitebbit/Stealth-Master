using System;
using System.Collections;
using UnityEngine;

namespace _3._Scripts
{
    [RequireComponent(typeof(Collider))]
    public class ColliderEnabler: MonoBehaviour
    {
        private Collider coll;

        private void Awake()
        {
            coll = GetComponent<Collider>();
            coll.enabled = true;
        }

        private void OnEnable()
        {
            coll.enabled = true;
        }

        private void Start()
        {
            coll.enabled = true;
            StartCoroutine(EnableWhile());
        }

        private IEnumerator EnableWhile()
        {
            yield return new WaitForSeconds(1);
            while (!coll.enabled)
            {
                coll.enabled = true;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}