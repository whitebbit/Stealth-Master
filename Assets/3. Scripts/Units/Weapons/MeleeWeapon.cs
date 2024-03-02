using System;
using _3._Scripts.OverlapSystem;
using _3._Scripts.OverlapSystem.Base;
using _3._Scripts.Units.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons
{
    public class MeleeWeapon: Weapon
    {
        public OverlapDetector<IWeaponVisitor> overlapDetector;

        private void Start()
        {
            overlapDetector.OnFound += Attack;
        }
        
    }
}