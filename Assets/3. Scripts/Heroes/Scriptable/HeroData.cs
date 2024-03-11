using _3._Scripts.Heroes.Perks.Scriptable;
using UnityEngine;
using YG;

namespace _3._Scripts.Heroes.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Heroes/Data", fileName = "HeroesData")]
    public class HeroData : ScriptableObject
    {
        [SerializeField] private string id;
        [Header("Main")] [SerializeField] private int startHealth;
        [Header("Perks")] 
        [SerializeField] private PerkData firstPerk;
        [SerializeField] private PerkData secondPerk;
        [Header("UI")] [SerializeField] private SerializableDictionary<string, string> name;
        [Space] [SerializeField] private Sprite miniIcon;
        [SerializeField] private Sprite bigIcon;

        public string ID => id;
        public int StartHealth => startHealth;
        
        public string Name => name[YandexGame.lang];
        public Sprite MiniIcon => miniIcon;
        public Sprite BigIcon => bigIcon;

        public PerkData FirstPerk => firstPerk;
        public PerkData SecondPerk => secondPerk;
    }
}