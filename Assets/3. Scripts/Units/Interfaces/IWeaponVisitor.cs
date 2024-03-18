
using UnityEngine;

namespace _3._Scripts.Units.Interfaces
{
    public interface IWeaponVisitor
    {
        public void Visit(float damage, Transform dealer = null);
        public Transform Target();
        public GameObject Object();
    }
}