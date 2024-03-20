using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Weapons.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/RangeWeaponData", fileName = "RangeWeaponData")]
    public class RangeWeaponData: ScriptableObject
    {
        [SerializeField] private int bulletCount;
        [SerializeField] private float spreadFactor;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float force;

        [Space] [SerializeField] private Bullet bullet;

        public int BulletCount => bulletCount;

        public float SpreadFactor => spreadFactor;

        public float AttackSpeed => attackSpeed;

        public Bullet Bullet => bullet;

        public float Force => force;
    }
}