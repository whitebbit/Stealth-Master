using System.Collections.Generic;
using UnityEngine;

namespace _3._Scripts.LevelManager.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/LevelManager/Location", fileName = "Location")]
    public class Location: ScriptableObject
    {
        [SerializeField] private NameYG name;
        [SerializeField] private Sprite icon;
        [Space]
        [SerializeField] private List<Contract> contracts = new();

        public string Name => name.ToString();
        public Sprite Icon => icon;
        public List<Contract> Contracts => contracts;
    }
}