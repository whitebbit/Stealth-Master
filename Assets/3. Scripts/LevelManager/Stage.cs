using System;
using _3._Scripts.LevelManager.Enums;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.LevelManager
{
    [Serializable]
    public class Stage
    {
        [SerializeField] private StageType type;
        [field:SerializeField]public string Name { get; set; }
        #if UNITY_EDITOR
        [SerializeField] private SceneAsset scene;
        #endif
        public StageType Type => type;

#if UNITY_EDITOR
        public void SetLevelName()
        {
            if (scene == null)
                throw new InvalidOperationException("Set scene for load scene name");
            
            Name = scene.name;
        }
#endif
    }
}