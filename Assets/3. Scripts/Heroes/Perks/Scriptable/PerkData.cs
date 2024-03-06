using UnityEngine;
using YG;

namespace _3._Scripts.Heroes.Perks.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Heroes/Perks/Data", fileName = "PerkData")]
    public class PerkData: ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, string> name;
        [SerializeField] private Sprite icon;

        public string Name => name[YandexGame.lang];
        public Sprite Icon => icon;
    }
}