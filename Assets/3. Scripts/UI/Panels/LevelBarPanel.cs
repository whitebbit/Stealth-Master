using System;
using _3._Scripts.UI.Interfaces;
using _3._Scripts.UI.Transitions;
using UnityEngine;

namespace _3._Scripts.UI.Panels
{
    public class LevelBarPanel : UIPanel
    {
        [SerializeField] private FadeTransition transition;
        protected override IUITransition InTransition { get; set; }
        protected override IUITransition OutTransition { get; set; }

        public override void Initialize()
        {
            InTransition = transition;
            OutTransition = transition;
        }
    }
}