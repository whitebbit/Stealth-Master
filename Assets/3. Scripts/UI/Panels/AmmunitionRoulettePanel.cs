using System.Collections.Generic;
using _3._Scripts.Environments;
using _3._Scripts.UI.Panels.Base;
using _3._Scripts.UI.Scriptable.Roulette;
using _3._Scripts.UI.Structs;
using _3._Scripts.UI.Utils;
using _3._Scripts.UI.Widgets;
using _3._Scripts.Units.Weapons.Scriptable;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _3._Scripts.UI.Panels
{
    public class AmmunitionRoulettePanel : SimplePanel
    {
        [Header("Components")] [SerializeField]
        private List<RouletteTable> tables = new();

        [SerializeField] private Button continueButton;
        [SerializeField] private AmmunitionRouletteTable tablePrefab;
        [Header("Rewards")] [SerializeField] private List<RouletteItem> items = new();
        [SerializeField] private List<RouletteItem> adItems = new();

        private Tween continueButtonTween;

        protected override void OnOpen()
        {
            var count = 10;
            for (var index = 0; index < tables.Count; index++)
            {
                var table = tables[index];
                var list = index == tables.Count - 1 ? adItems : items;
                table.StartRoulette(tablePrefab, count, list);
                count += 4;
            }

            InitializeContinueButton();
        }

        protected override void OnClose()
        {
            continueButtonTween.Pause();
            continueButtonTween.Kill();
            continueButtonTween = null;
        }

        private void InitializeContinueButton()
        {
            continueButtonTween = continueButton.targetGraphic.DOFade(0.25f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(continueButton.gameObject);

            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(() =>
            {
                Stage.Instance.OnPlay = true;
                UIManager.Instance.GetPanel<AmmunitionRoulettePanel>().Enabled = false;
                UIManager.Instance.GetWidget<FreeMoneyWidget>().Enabled = false;
                UIManager.Instance.GetPanel<JoystickPanel>().Enabled = true;
                UIManager.Instance.GetPanel<UnitCounterPanel>().Enabled = true;
            });
        }
    }
}