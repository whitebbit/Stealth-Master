using System;
using _3._Scripts.LevelManager.Enums;
using UnityEngine;

namespace _3._Scripts.UI.Utils
{
    [Serializable]
    public class LevelBarSection
    {
        [SerializeField] private StageType type;
        [SerializeField] private Sprite sprite;

        public StageType Type => type;
        public Sprite Sprite => sprite;
    }
}