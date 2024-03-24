using System;
using System.Collections.Generic;
using _3._Scripts.Singleton;
using _3._Scripts.UI;
using _3._Scripts.UI.Widgets;
using _3._Scripts.Units.Bots;
using UnityEngine;

namespace _3._Scripts.Environments
{
    public class Stage : Singleton<Stage>
    {
        public bool Alarm
        {
            get => alarm;
            set
            {
                alarm = value;
                OnAlarm?.Invoke(value);
            }
        }
        public bool OnPlay { get; set; }
        public int UnitsCount
        {
            get => unitsCount;
            set
            {
                unitsCount = value;
                OnUnitsCountChange?.Invoke(unitsCount);
            }
        }
        public event Action<bool> OnAlarm;
        public event Action<int> OnUnitsCountChange;

        private bool alarm;
        private int unitsCount;
        private readonly List<Bot> bots = new();

        private void Start()
        {
           
        }

        public void AddBot(Bot bot)
        { 
            bots.Add(bot);
            unitsCount++;
        }
        public void RemoveBot(Bot bot) => bots.Remove(bot);
        public bool ContainsBot(Bot bot)
        {
            return bots.Contains(bot);
        }
        
        
    }
}