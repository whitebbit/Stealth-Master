using _3._Scripts.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.UI
{
    public abstract class UIElement: MonoBehaviour, IUIElement
    {
        private bool _enabled;
        private bool onTransition;
        public abstract IUITransition InTransition { get; set; }
        public abstract IUITransition OutTransition { get; set; }
        
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (value == _enabled) return;

                if (value) Open();
                else Close();
            }
        }
        public abstract void Initialize();

        public virtual void ForceOpen()
        {
            gameObject.SetActive(true);
            InTransition.ForceIn();
            _enabled = true;
            OnOpen();
        }
        public virtual void ForceClose()
        {
            OnClose();
            OutTransition.ForceOut();
            _enabled = false;
            gameObject.SetActive(false);
        }

        private void Open()
        {
            if(onTransition) return;
            
            onTransition = true;
            gameObject.SetActive(true);
            InTransition.AnimateIn().OnComplete(() =>
            {
                _enabled = true;
                onTransition = false;
            });
            OnOpen();
        }
        private void Close()
        {
            if(onTransition) return;
            
            onTransition = true;
            OutTransition.AnimateOut().OnComplete(() =>
            {
                OnClose();
                _enabled = false;
                onTransition = false;
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