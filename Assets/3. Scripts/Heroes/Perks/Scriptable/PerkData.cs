using UnityEngine;

namespace _3._Scripts.Heroes.Perks.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/Heroes/Perks/Data", fileName = "PerkData")]
    public class PerkData: ScriptableObject
    {
        [SerializeField] private NameYG name;
        [SerializeField] private Sprite icon;

        public string Name => name.Name;
        public Sprite Icon => icon;
    }
}