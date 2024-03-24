using System;
using _3._Scripts.UI;
using _3._Scripts.UI.Widgets;
using UnityEngine;

namespace _3._Scripts.Environments
{
    public class GroundStart: MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.CurrentScreen.Open();
            UIManager.Instance.GetWidget<DollarWidget>(0).Enabled = true;
            UIManager.Instance.GetWidget<TokenWidget>(0).Enabled = true;
            UIManager.Instance.GetWidget<DiamondsWidget>(0).Enabled = true;
        }
    }
}