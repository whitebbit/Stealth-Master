using System;
using System.Collections.Generic;
using _3._Scripts.LevelManager;
using _3._Scripts.LevelManager.Scriptable;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace _3._Scripts.Configurations
{
    public class LevelConfiguration : MonoBehaviour
    {
        [SerializeField] private List<Location> locations = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            LevelHandler.SetLocations(locations);
        }

        private void Start()
        {
            SceneManager.LoadScene(LevelHandler.CurrentStage().Name);
        }
    }
}