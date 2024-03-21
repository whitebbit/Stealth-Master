using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using UnityEngine;

namespace _3._Scripts.UI.Widgets
{
    public class WalletWidget: UIWidget
    {
        [SerializeField] private FadeTransition transition;
        [SerializeField] private ManyObjectsSlideTransition playModeTransition;
        public override IUITransition InTransition { get; set; }
        public override IUITransition OutTransition { get; set; }
        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
        }

        public bool PlayMode
        {
            set
            {
                if (value)
                    playModeTransition.AnimateIn();
                else
                    playModeTransition.AnimateOut();
            }
        }
    }
}