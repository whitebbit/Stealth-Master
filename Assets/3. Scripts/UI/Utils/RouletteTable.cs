using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Enums;
using _3._Scripts.UI.Scriptable.Roulette;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Utils
{
    [Serializable]
    public class RouletteTable : MonoBehaviour
    {
        [SerializeField] private HorizontalOrVerticalLayoutGroup container;

        [Header("Money Button")] [SerializeField]
        private Button moneyButton;

        [SerializeField] private TMP_Text price;
        [Header("AD Button")] [SerializeField] private Button adButton;

        private CanvasGroup moneyButtonCanvasGroup;
        private CanvasGroup adButtonCanvasGroup;
        private RouletteItem reward;

        private void Awake()
        {
            moneyButtonCanvasGroup = moneyButton.GetComponent<CanvasGroup>();
            adButtonCanvasGroup = adButton.GetComponent<CanvasGroup>();
        }

        public void Start()
        {
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            moneyButtonCanvasGroup.alpha = 0;
            moneyButtonCanvasGroup.blocksRaycasts = false;
            adButtonCanvasGroup.alpha = 0;
            adButtonCanvasGroup.blocksRaycasts = false;
        }

        public void StartRoulette(AmmunitionRouletteTable tablePrefab, int itemCount, List<RouletteItem> items)
        {
            var rewardTable = GetReward(tablePrefab, items);

            SpawnTables(tablePrefab, itemCount, items);

            if (GetRectTransform(tablePrefab, itemCount, out var t, out var height)) return;
            t.DOAnchorPos(new Vector2(t.anchoredPosition.x, -(height / 2)), 0.15f * itemCount).SetEase(Ease.OutBack)
                .OnComplete(
                    () =>
                    {
                        InitializeButton();
                        InitializeTable(rewardTable);
                    });
        }

        private void InitializeTable(AmmunitionRouletteTable rewardTable)
        {
            price.text = $"{reward.Price}";
            rewardTable.ShowTitle();
        }

        private bool GetRectTransform(AmmunitionRouletteTable tablePrefab, int itemCount, out RectTransform t, out float height)
        {
            t = container.transform as RectTransform;
            height = (tablePrefab.Rect.sizeDelta.y + container.spacing) * itemCount - container.spacing;
            if (t == null) return true;

            t.sizeDelta = new Vector2(t.sizeDelta.x, height);
            t.anchoredPosition = Vector2.zero;
            return false;
        }

        private void SpawnTables(AmmunitionRouletteTable tablePrefab, int itemCount, List<RouletteItem> items)
        {
            for (var i = 0; i < itemCount - 1; i++)
            {
                var table = Instantiate(tablePrefab, container.transform);
                table.Initialize(GetRandomItem(items));
            }
        }
        private void InitializeButton()
        {
            var canvasGroup = reward.BuyType == BuyType.Money ? moneyButtonCanvasGroup : adButtonCanvasGroup;
            var button = reward.BuyType == BuyType.Money ? moneyButton : adButton;
            
            canvasGroup.DOFade(1, 0.25f);
            canvasGroup.blocksRaycasts = true;
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(reward.OnReward);
        }
         
        private AmmunitionRouletteTable GetReward(AmmunitionRouletteTable tablePrefab, List<RouletteItem> items)
                {
                    var rewardTable = Instantiate(tablePrefab, container.transform);
                    reward = GetRandomItem(items);
                    rewardTable.Initialize(reward);
                    
                    return rewardTable;
                }

        public RouletteItem GetRandomItem(List<RouletteItem> items)
        {
            items = items.Where(i => i.Unlocked()).ToList();
            var totalWeight = items.Sum(obj => 1f / (float)obj.Rarity);
            var randomValue = UnityEngine.Random.Range(0f, totalWeight);
            var weightSum = 0f;

            foreach (var obj in items)
            {
                weightSum += 1f / (float)obj.Rarity;
                if (randomValue <= weightSum) return obj;
            }

            return null;
        }
    }
}