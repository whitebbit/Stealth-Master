using System;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Weapons
{
    public class Bullet: MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Transform model;
        [Header("FX")] [SerializeField] private ParticleSystem decal;
        private float damage;

        public void SetDamage(float value) => damage = value;
        public void AddForce(Vector3 direction, float force)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
            Destroy(gameObject, 5);
        }

        private void OnCollisionEnter(Collision other)
        {
            
            Debug.Log(other.gameObject.name);
            if (other.gameObject.TryGetComponent(out IWeaponVisitor visitor))
            {
                visitor.Visit(damage);
                Destroy(gameObject);
            }
            else
            {
                decal.Play();
                model.gameObject.SetActive(false);
                rigidbody.isKinematic = true;
                Destroy(gameObject, 3);
            }
        }
    }
}