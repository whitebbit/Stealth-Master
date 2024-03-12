using System.Collections.Generic;
using _3._Scripts.Units.Weapons;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class PlayerAmmunition : MonoBehaviour
    {
        [SerializeField] private List<Weapon> weapons = new();

        public void SetWeapon(string id = "default")
        {
            foreach (var weapon in weapons)
            {
                weapon.gameObject.SetActive(weapon.ID == id);
            }
        }
    }
}