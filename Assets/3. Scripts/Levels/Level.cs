using System;
using System.Collections.Generic;
using _3._Scripts.Singleton;
using _3._Scripts.Units.Bots;
using UnityEngine;

namespace _3._Scripts.Levels
{
    public class Level : Singleton<Level>
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

        public event Action<bool> OnAlarm;

        private bool alarm;
        private readonly List<Bot> bots = new();

        public void AddBot(Bot bot) => bots.Add(bot);
        public void RemoveBot(Bot bot) => bots.Remove(bot);

        public bool ContainsBot(Bot bot)
        {
            return bots.Contains(bot);
        }
    }
}