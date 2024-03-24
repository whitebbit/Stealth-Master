using _3._Scripts.Heroes.Scriptable;
using _3._Scripts.Units.Weapons.Scriptable;
using UnityEngine;
using YG;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    [CreateAssetMenu(menuName = "Configs/Roulette/HeroWeaponRouletteItem", fileName = "HeroWeaponRouletteItem",
        order = 0)]
    public class HeroWeaponRouletteItem : WeaponRouletteItem
    {
        [SerializeField] private HeroData heroData;
        [SerializeField] private int levelToUnlock;

        public override bool Unlocked()
        {
            var exist = YandexGame.savesData.playerSave.heroes.ExistHero(heroData.ID);
            if (!exist) return false;

            var isCurrent = YandexGame.savesData.playerSave.heroes.currentHero == heroData.ID;
            if (!isCurrent) return false;

            var level = YandexGame.savesData.playerSave.heroes.GetHero(heroData.ID).level >= levelToUnlock;

            return level;
        }

        public HeroData HeroData => heroData;

        public override void OnReward()
        {
        }
    }
}