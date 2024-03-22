using System;
using _3._Scripts.LevelManager.Enums;
using UnityEngine;

namespace _3._Scripts.UI.Utils
{
    [Serializable]
    public class LevelBarSection
    {
        [SerializeField] private LevelType type;
        [SerializeField] private Sprite sprite;

        public LevelType Type => type;
        public Sprite Sprite => sprite;
    }
}