using _3._Scripts.Environments;
using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Panels
{
    public class UnitCounterPanel : UIPanel
    {
        [SerializeField] private SlideTransition transition;
        [SerializeField] private TMP_Text counter;
        [SerializeField] private Image completed;
        public override IUITransition InTransition { get; set; }
        public override IUITransition OutTransition { get; set; }

        private int lastCount;

        public override void Initialize()
        {
            transition.SetStartPosition();
            InTransition = transition;
            OutTransition = transition;
            completed.DOFade(0, 0);
            
            Stage.Instance.OnUnitsCountChange += UpdateCounter;
        }

        protected override void OnOpen()
        {
            UpdateCounter(Stage.Instance.UnitsCount);
        }

        private void UpdateCounter(int count)
        {
            counter.DOCounter(lastCount, count, 0.25f).OnComplete(() =>
            {
                if (count > 0) return;
                completed.DOFade(1, 0.25f);
                counter.DOFade(0, 0.15f);
            });
            lastCount = count;
        }
    }
}