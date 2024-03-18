using System;
using UnityEngine;
using YG;

namespace _3._Scripts
{
    [Serializable]
    public class NameYG
    {
        [SerializeField] private SerializableDictionary<string, string> name;

        public string Name => name[YandexGame.lang];
    }
}