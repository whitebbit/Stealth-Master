using System;
using UnityEngine;
using YG;

namespace _3._Scripts.Configurations
{
    public class PlayerConfiguration : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            YandexGame.savesData.playerSave.heroes.UnlockHero("ninja");
        }
    }
}