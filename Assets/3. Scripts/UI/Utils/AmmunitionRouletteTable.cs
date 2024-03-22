using _3._Scripts.UI.Scriptable.Roulette;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Utils
{
    public class AmmunitionRouletteTable: MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        public RectTransform Rect => rect;

        public void Initialize(RouletteItem item)
        {
            title.text = item.Title.ToUpper();
            title.DOFade(0, 0);
            icon.sprite = item.Icon;
            background.sprite = item.Background;
        }

        public void ShowTitle()
        {
            title.DOFade(1, 0.25f);
        }
    }
}