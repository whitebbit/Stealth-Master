using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using UnityEngine;

namespace _3._Scripts.UI.Panels
{
    public class WalletPanel: UIPanel
    {
        [SerializeField] private ManyObjectsSlideTransition transition;
        protected override IUITransition InTransition { get; set; }
        protected override IUITransition OutTransition { get; set; }
        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
        }
    }
}