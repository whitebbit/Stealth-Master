using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour
    {
        public string ID => id;
        public bool Blocked { get; private set; }
        
        [SerializeField] private string id;
        [SerializeField] private UIPanel[] defaultPanels = Array.Empty<UIPanel>();
        
        private UIPanel[] panels;
        private bool opened;
        private CanvasGroup canvasGroup;
        
        public void Initialize()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            panels = GetComponentsInChildren<UIPanel>();

            foreach (var panel in panels)
            {
                panel.Initialize();
                panel.ForceClose();
            }
            
            opened = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }

        public void Open(float duration = 0.1f, TweenCallback onStart = null, TweenCallback onComplete = null)
        {
            if (opened) return;

            gameObject.SetActive(true);

            canvasGroup.DOFade(1, duration).OnStart(() =>
            {
                opened = true;
                onStart?.Invoke();
                transform.SetAsLastSibling();
                foreach (var panel in defaultPanels) panel.Opened = true;

            }).OnComplete(() =>
            {
                canvasGroup.blocksRaycasts = true;
                onComplete?.Invoke();
            });
        }

        public void Close(float duration = 0.1f, TweenCallback onStart = null, TweenCallback onComplete = null)
        {
            if (!opened) return;
            
            canvasGroup.DOFade(0, duration).OnStart(() =>
            {
                canvasGroup.blocksRaycasts = false;
                opened = false;
                foreach (var panel in panels) panel.Opened = false;
                onStart?.Invoke();
            }).OnComplete(() =>
            {
                onComplete?.Invoke();
                transform.SetAsFirstSibling();
                gameObject.SetActive(false);
            });
        }

        public T GetPanel<T>() where T: UIPanel
        {
            return (T)panels.FirstOrDefault(p => p is T);
        }
    }
}