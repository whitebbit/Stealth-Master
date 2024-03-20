using System;
using _3._Scripts.LevelManager.Enums;
using UnityEditor;
using UnityEngine;

namespace _3._Scripts.LevelManager
{
    [Serializable]
    public class Level
    {
        [SerializeField] private LevelType type;
        [SerializeField] private SceneAsset scene;
        
        public LevelType Type => type;
        public string Name => scene.name;
    }
}