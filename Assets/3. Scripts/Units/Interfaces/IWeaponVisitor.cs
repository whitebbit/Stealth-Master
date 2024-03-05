
using UnityEngine;

namespace _3._Scripts.Units.Interfaces
{
    public interface IWeaponVisitor
    {
        public void Visit(float damage);
        public Transform Transform();
    }
}