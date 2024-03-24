using System;
using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI.Transitions
{
    [Serializable]
    public class FadeTransition : IUITransition
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [Space]
        [SerializeField] private float duration;
        public Tween AnimateIn()
        {
            canvasGroup.blocksRaycasts = true;
            return canvasGroup.DOFade(1, duration).SetLink(canvasGroup.gameObject);
        }

        public Tween AnimateOut()
        {
            canvasGroup.blocksRaycasts = false;
            return canvasGroup.DOFade(0, duration).SetLink(canvasGroup.gameObject);
        }

        public void ForceIn()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }

        public void ForceOut()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }
}