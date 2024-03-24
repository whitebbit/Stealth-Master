using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _3._Scripts.UI.Widgets
{
    public class FreeMoneyWidget : UIWidget
    {
        [SerializeField] private FadeTransition transition;
        [Space] [SerializeField] private int count;
        [Space] [SerializeField] private TMP_Text text;
        [SerializeField] private Button button;
        [SerializeField] private RectTransform rays;
        public override IUITransition InTransition { get; set; }
        public override IUITransition OutTransition { get; set; }

        private Tween raysTween;

        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
        }

        protected override void OnOpen()
        {
            raysTween = rays.DORotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360)
                .SetRelative(true)
                .SetLoops(-1)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);

            text.text = $"+{count}";
        }

        protected override void OnClose()
        {
            raysTween.Pause();
            raysTween.Kill();
            raysTween = null;
        }
    }
}