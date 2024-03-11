using UnityEngine;
using YG;

namespace _3._Scripts.Units.Weapons.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Weapons/Data", fileName = "WeaponData")]
    public class WeaponData: ScriptableObject
    {
        [SerializeField] private string id;
        [Header("Base")]
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [Header("Animation")]
        [SerializeField] private AnimatorOverrideController animatorController;

        [Header("UI")] [SerializeField] private SerializableDictionary<string, string> name;
        [SerializeField] private Sprite icon;
        public string ID => id;
        public float AttackCooldown => attackCooldown;
        public float Damage => damage;
        public AnimatorOverrideController AnimatorController => animatorController;

        public string Name => name[YandexGame.lang];

        public Sprite Icon => icon;
    }
}