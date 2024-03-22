using System;
using UnityEngine;

namespace _3._Scripts.LevelManager
{
    [Serializable]
    public class FinalLevelData
    {
        [SerializeField] private NameYG name;
        [SerializeField] private Sprite icon;

        public Sprite Icon => icon;

        public string Name => name.ToString();
    }
}