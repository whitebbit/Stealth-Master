using System;
using _3._Scripts.Heroes.Perks;
using _3._Scripts.Heroes.Scriptable;
using _3._Scripts.Units.Player;
using UnityEngine;
using YG;

namespace _3._Scripts.Heroes
{
    public abstract class Hero : MonoBehaviour
    {
        [SerializeField] protected HeroData data;

        protected Perk FirstPerk;
        protected Perk SecondPerk;

        private Player player;

        private int Level => YandexGame.savesData.playerSave.heroes.GetHero(data.ID) == null
            ? 0
            : YandexGame.savesData.playerSave.heroes.GetHero(data.ID).level;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Start()
        {
            player.Health.SetMaxHealth(data.StartHealth);

            InitializePerks();
            ActivatePerks();
        }

        protected abstract void InitializePerks();

        private void ActivatePerks()
        {
            if (Level >= 4)
                FirstPerk.Activate();
            if (Level >= 9)
                SecondPerk.Activate();
        }
    }
}