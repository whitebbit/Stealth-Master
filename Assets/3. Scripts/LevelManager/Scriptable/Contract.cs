using System;
using System.Collections.Generic;
using _3._Scripts.LevelManager.Enums;
using UnityEngine;

namespace _3._Scripts.LevelManager.Scriptable
{
    [CreateAssetMenu(menuName = "Configs/LevelManager/Contract", fileName = "Contract")]
    public class Contract : ScriptableObject
    {
        [SerializeField] private NameYG name;
        [Space]
        [SerializeField] private List<Level> levels = new();
        [SerializeField] private FinalLevelData finalLevelData;
        
        public string Name => name.Name;
        public List<Level> Levels => levels;
        public FinalLevelData FinalLevelData => finalLevelData;

#if  UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var level in levels)
            {
                level.SetLevelName();
            }
        }
#endif
    }
}