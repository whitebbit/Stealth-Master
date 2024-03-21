using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using UnityEngine;

namespace _3._Scripts.UI.Panels
{
    public class SimplePanel: UIPanel
    {
        [SerializeField] private FadeTransition transition;
        public override IUITransition InTransition { get; set; }
        public override IUITransition OutTransition { get; set; }
        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
        }
    }
}