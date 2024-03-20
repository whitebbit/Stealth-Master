using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        protected abstract IUITransition InTransition { get; set; }
        protected abstract IUITransition OutTransition { get; set; }

        private bool opened;

        public bool Opened
        {
            set
            {
                if (value == opened) return;

                opened = value;

                if (opened) Open();
                else Close();
            }
        }

        public abstract void Initialize();
        
        public virtual void ForceOpen()
        {
            gameObject.SetActive(true);
            opened = true;
            InTransition.ForceIn();
            OnOpen();
        }
        
        public virtual void ForceClose()
        {
            opened = false;
            OutTransition.ForceOut();
            OnClose();
            gameObject.SetActive(false);
        }
        
        private void Open()
        {
            gameObject.SetActive(true);
            OnOpen();
            InTransition.AnimateIn();
        }

        private void Close()
        {
            OutTransition.AnimateOut().OnComplete(() =>
            {
                OnClose();
                gameObject.SetActive(false);
            });
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }
    }
}