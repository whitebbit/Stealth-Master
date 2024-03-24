using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Scriptable.Roulette;
using _3._Scripts.UI.Structs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Utils
{
    public class AmmunitionRouletteTable : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private List<RarityTable> rarityTables = new();

        [Header("Table Components")] [SerializeField]
        private RectTransform rect;

        [SerializeField] private TMP_Text title;
        [SerializeField] private Image icon;
        [SerializeField] private Image background;

        [Header("Special Icon Components")] [SerializeField]
        private RectTransform specialIconTransform;

        [SerializeField] private Image heroIcon;

        public RectTransform Rect => rect;

        public void Initialize(RouletteItem item)
        {
            title.text = item.Title.ToUpper();
            title.DOFade(0, 0);
            icon.sprite = item.Icon;
            background.sprite = rarityTables.FirstOrDefault(t => t.Rarity == item.Rarity).Table;


            specialIconTransform.gameObject.SetActive(false);
            if (item is not HeroWeaponRouletteItem rouletteItem) return;
            specialIconTransform.gameObject.SetActive(true);
            heroIcon.sprite = rouletteItem.HeroData.MiniIcon;
        }

        public void ShowTitle()
        {
            title.DOFade(1, 0.25f);
        }
    }
}