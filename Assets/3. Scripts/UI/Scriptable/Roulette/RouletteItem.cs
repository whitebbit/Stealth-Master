using _3._Scripts.UI.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.UI.Scriptable.Roulette
{
    public abstract class RouletteItem: ScriptableObject
    {
        [SerializeField] private Rarity rarity;
        [SerializeField] private BuyType buyType;
        [SerializeField] private int price;
        public abstract string Title { get; }
        public abstract Sprite Icon { get; }
        public int Price => price;
        public Rarity Rarity => rarity;
        public BuyType BuyType => buyType;
        public abstract bool Unlocked();
        public abstract void OnReward();
    }
}