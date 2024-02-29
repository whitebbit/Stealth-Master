using _3._Scripts.Controls.Interfaces;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units
{
    public abstract class Unit: MonoBehaviour
    {
        public UnitHealth Health { get; protected set; }
        public IDamageable Damageable { get; protected set; }
    }
}