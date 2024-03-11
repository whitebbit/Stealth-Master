using System;
using System.Collections.Generic;
using System.Linq;
using YG;

namespace _3._Scripts.Saves
{
    [Serializable]
    public class HeroesSave
    {
        public List<HeroSave> unlockedHeroes = new();

        public HeroSave GetHero(string id)
        {
            return unlockedHeroes.FirstOrDefault(s => s.id == id);
        }

        public void UpgradeHero(string id)
        {
            var save = unlockedHeroes.FirstOrDefault(s => s.id == id);
            if (save == null) return;

            save.level++;
            YandexGame.SaveProgress();
        }

        public void UnlockHero(string id)
        {
            if (unlockedHeroes.Exists(s => s.id == id)) return;
            unlockedHeroes.Add(new HeroSave
            {
                id = id,
                level = 1
            });
            YandexGame.SaveProgress();
        }
    }
}