using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.LevelManager;
using _3._Scripts.LevelManager.Enums;
using _3._Scripts.LevelManager.Scriptable;
using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Panels.Base;
using _3._Scripts.UI.Transitions;
using _3._Scripts.UI.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Panels
{
    public class LevelBarPanel : SimplePanel
    {
        [Header("Sections")]
        [SerializeField] private List<LevelBarSection> levelBarSections = new();
        [SerializeField] private Image sectionPrefab;
        [SerializeField] private RectTransform container;
        [Space] [SerializeField] private Color completed;
        [SerializeField] private Color current;
        [Header("Title")] [SerializeField] private TMP_Text title;
        [Header("Final")] 
        [SerializeField] private RectTransform finalContainer;
        [SerializeField] private Image finalIcon;
        private readonly List<Image> sections = new();

        protected override void OnOpen()
        {
            var contract = LevelHandler.CurrentContract();
            
            SpawnSections(contract);
            SetSize();
            SetInfo(contract);
            sections[LevelHandler.CurrentLevelIndex+1].DOColor(current, 0.5f).SetLoops(-1, LoopType.Yoyo);
            for (var i = 0; i < LevelHandler.CurrentLevelIndex+1; i++)
            {
                sections[i].color = completed;
            }
        }

        private void SetInfo(Contract contract)
        {
            finalIcon.sprite = contract.FinalLevelData.Icon;
            title.text = contract.Name;
        }
        private void SetSize()
        {
            var t = transform as RectTransform;
            var width = sections.Sum(s => ((RectTransform)s.transform).sizeDelta.x);
            
            width += finalContainer.sizeDelta.x;
            width += sections.Count * 10;

            if (t != null) t.sizeDelta = new Vector2(width, t.sizeDelta.y);
        }
        private void SpawnSections(Contract contract)
        {
            foreach (var level in contract.Levels.Where(l => l.Type != LevelType.Boss))
            {
                var section = Instantiate(sectionPrefab, container);
                var sprite = levelBarSections.Find(l => l.Type == level.Type).Sprite;
                section.sprite = sprite;
                section.SetNativeSize();
                sections.Add(section);
            }
        }
    }
}