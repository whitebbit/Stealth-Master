using _3._Scripts.Units.Weapons.Scriptable;
using UnityEngine;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    [CreateAssetMenu(menuName = "Configs/Roulette/WeaponRouletteItem", fileName = "WeaponRouletteItem", order = 0)]
    public class WeaponRouletteItem: RouletteItem
    {
        [SerializeField] private WeaponData data;
        
        public override string Title => data.Name;
        public override Sprite Icon  => data.Icon;
        public override void OnReward()
        {
            
        }
    }
}