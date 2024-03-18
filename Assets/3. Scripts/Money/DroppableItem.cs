using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3._Scripts.Money
{
    [RequireComponent(typeof(Rigidbody))]
    public class DroppableItem : MonoBehaviour
    {
        [SerializeField] private float horizontalForce = 50;
        
        private Collider collider;
        private Rigidbody rigidbody;

        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
        }

        public void Push()
        {
            var random = Random.insideUnitCircle;
            var shift = new Vector3(random.x, 0, random.y);
            Push(Vector3.up + shift * horizontalForce);
        }

        public void Push(Vector3 direction)
        {
            rigidbody.AddForce(direction, ForceMode.VelocityChange);
            StartCoroutine(DisableBodyWhenReady());
        }

        public void DisableGravity()
        {
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            collider.enabled = false;
        }
        public void DisableCollision()
        {
            collider.enabled = false;
        }
        private IEnumerator DisableBodyWhenReady()
        {
            yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => rigidbody.velocity == Vector3.zero);

            rigidbody.isKinematic = true;
        }
    }
}