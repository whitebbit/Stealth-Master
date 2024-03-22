using System.Collections.Generic;
using _3._Scripts.UI.Panels.Base;
using _3._Scripts.UI.Scriptable.Roulette;
using _3._Scripts.UI.Utils;
using _3._Scripts.Units.Weapons.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _3._Scripts.UI.Panels
{
    public class AmmunitionRoulettePanel : SimplePanel
    {
        [SerializeField] private List<RouletteTable> container;
        [SerializeField] private AmmunitionRouletteTable tablePrefab;
        [SerializeField] private RouletteItem rouletteItem;
        [SerializeField] private Ease ease;

        protected override void OnOpen()
        {
            var count = 4;
            foreach (var cGroup in container)
            {
                cGroup.Initialize();
                cGroup.StartRoulette(tablePrefab, count, rouletteItem);
                count += 2;
            }
        }
        
    }
}