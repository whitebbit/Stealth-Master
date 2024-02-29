using UnityEngine;

namespace _3._Scripts.Units.Weapons
{
    public class MeleeWeapon: Weapon
    {
        protected override void PerformAttack()
        {
            Debug.Log("Attack");
        }
    }
}