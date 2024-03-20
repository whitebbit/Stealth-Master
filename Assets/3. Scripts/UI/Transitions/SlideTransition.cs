using System;
using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI.Transitions
{
    [Serializable]
    public class SlideTransition : IUITransition
    {
        [SerializeField] private RectTransform transform;
        [Space] [SerializeField] private float duration;
        [SerializeField] private Vector2 direction;

        public Tween AnimateIn()
        {
            return transform.DOAnchorPos(transform.anchoredPosition + direction, duration);
        }

        public Tween AnimateOut()
        {
            return transform.DOAnchorPos(transform.anchoredPosition - direction, duration);
        }

        public void ForceIn()
        {
            transform.anchoredPosition += direction;
        }

        public void ForceOut()
        {
            transform.anchoredPosition -= direction;
        }
    }
}