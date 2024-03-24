using System;
using System.Collections.Generic;
using _3._Scripts.LevelManager.Scriptable;
using YG;

namespace _3._Scripts.LevelManager
{
    public static class LevelHandler
    {
        public static int CurrentLocationIndex
        {
            get => YandexGame.savesData.levelSaves.currentLocation;
            private set
            {
                YandexGame.savesData.levelSaves.currentLocation = value;
                OnLocationChange?.Invoke();
            }
        }

        public static int CurrentContractIndex
        {
            get => YandexGame.savesData.levelSaves.currentContract;
            private set
            {
                if (value >= CurrentLocation().Contracts.Count)
                {
                    YandexGame.savesData.levelSaves.currentContract = 0;
                    CurrentLocationIndex += 1;
                }
                else YandexGame.savesData.levelSaves.currentContract = value;

                OnContractChange?.Invoke();
            }
        }

        public static int CurrentStageIndex
        {
            get => YandexGame.savesData.levelSaves.currentLevel;
            set
            {
                if (value >= CurrentContract().Stages.Count)
                {
                    YandexGame.savesData.levelSaves.currentLevel = 0;
                    CurrentContractIndex += 1;
                }
                else YandexGame.savesData.levelSaves.currentLevel = value;
                OnStageChange?.Invoke();
            }
        }

        public static event Action OnStageChange;
        public static event Action OnContractChange;
        public static event Action OnLocationChange;


        private static List<Location> _locations = new();

        public static void SetLocations(List<Location> locations) => _locations = locations;

        public static Stage CurrentStage()
        {
            var level = CurrentContract().Stages[CurrentStageIndex];
            return level;
        }
        public static Contract CurrentContract()
        {
            var contract = CurrentLocation().Contracts[CurrentContractIndex];
            return contract;
        }
        public static Location CurrentLocation()
        {
            var location = _locations[CurrentLocationIndex];
            return location;
        }
    }
}