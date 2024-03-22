using System;
using _3._Scripts.UI.Scriptable.Roulette;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace _3._Scripts.UI.Utils
{
    [Serializable]
    public class RouletteTable
    {
        [SerializeField] private HorizontalOrVerticalLayoutGroup container;
        [SerializeField] private Button button;
        [SerializeField] private CanvasGroup buttonCanvasGroup;

        public void Initialize()
        {
            buttonCanvasGroup.alpha = 0;
        }
        
        public void StartRoulette(AmmunitionRouletteTable tablePrefab, int itemCount, RouletteItem rouletteItem)
        {
            var reward = Object.Instantiate(tablePrefab, container.transform);
            reward.Initialize(rouletteItem);

            for (var i = 0; i < itemCount - 1; i++)
            {
                var table = Object.Instantiate(tablePrefab, container.transform);
                table.Initialize(rouletteItem);
            }

            var t = container.transform as RectTransform;
            var height = (tablePrefab.Rect.sizeDelta.y + container.spacing) * itemCount - container.spacing;
            if (t == null) return;

            t.sizeDelta = new Vector2(t.sizeDelta.x, height);
            t.anchoredPosition = Vector2.zero;
            t.DOAnchorPos(new Vector2(t.anchoredPosition.x, -(height / 2)), 0.35f * itemCount).SetEase(Ease.OutBack)
                .OnComplete(
                    () =>
                    {
                        buttonCanvasGroup.DOFade(1, 0.25f);
                        reward.ShowTitle();
                    });
        }
    }
}