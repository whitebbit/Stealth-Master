using System;
using _3._Scripts.Controls;
using _3._Scripts.Controls.Interfaces;
using _3._Scripts.Units.Damageable;
using _3._Scripts.Units.Health;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class Player: Unit
    {
        private void Awake()
        {
            Health = new UnitHealth(100);
            Damageable = new UnitDamageable(Health);
        }
    }
}