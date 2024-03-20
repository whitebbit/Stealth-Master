using System;
using System.Collections.Generic;
using _3._Scripts.LevelManager.Scriptable;
using UnityEngine;

namespace _3._Scripts.LevelManager
{
    public class Test: MonoBehaviour
    {
        [SerializeField] private List<Location> locations = new();

        private void Start()
        {
            LevelHandler.SetLocations(locations);
        }

        public void TestAction()
        {
            LevelHandler.CurrentLevelIndex += 1;
            Debug.Log(LevelHandler.CurrentLevel().Name);
            Debug.Log(LevelHandler.CurrentContract().Name);
            Debug.Log(LevelHandler.CurrentLocation().Name);
        }
    }
}