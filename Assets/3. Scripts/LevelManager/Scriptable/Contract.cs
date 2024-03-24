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
        [SerializeField] private List<Stage> stages = new();
        [SerializeField] private FinalLevelData finalLevelData;
        
        public string Name => name.ToString();
        public List<Stage> Stages => stages;
        public FinalLevelData FinalLevelData => finalLevelData;

#if  UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var stage in stages)
            {
                stage.SetLevelName();
            }
        }
#endif
    }
}