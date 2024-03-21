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
            TestAction();
        }

        public void TestAction()
        {
            Debug.Log(LevelHandler.CurrentLevel().Name);
            Debug.Log(LevelHandler.CurrentContract().Name);
            Debug.Log(LevelHandler.CurrentLocation().Name);
        }
    }
}