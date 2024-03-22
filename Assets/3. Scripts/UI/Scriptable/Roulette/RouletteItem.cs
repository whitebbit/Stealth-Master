using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    public abstract class RouletteItem: ScriptableObject
    {
        [SerializeField] private Sprite background;

        public Sprite Background => background;
        public abstract string Title { get; }
        public abstract Sprite Icon { get; }
        public abstract void OnReward();
    }
}