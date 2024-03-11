using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;
using YG;

namespace _3._Scripts.Saves
{
    [Serializable]
    public class PlayerSave
    {
        public WalletSave wallet = new();
        public HeroesSave heroes = new();
        public WeaponSave weapon = new();
    }
}