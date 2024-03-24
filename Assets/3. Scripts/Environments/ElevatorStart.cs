using System;
using _3._Scripts.CameraControllers;
using _3._Scripts.UI;
using _3._Scripts.UI.Widgets;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Environments
{
    public class ElevatorStart : MonoBehaviour
    {
        
        [SerializeField, Min(0)] private float doorOffset = 1.75f;
        [Space] [SerializeField] private Transform cabin;
        [Space] [SerializeField] private Transform rightDoor;
        [SerializeField] private Transform leftDoor;

        private void Start()
        {
            DoStart();
        }

        private void DoStart()
        {
            cabin.DOMoveY(-5, 1).From().OnComplete(() =>
            {
                UIManager.Instance.CurrentScreen.Open();
                UIManager.Instance.GetWidget<DollarWidget>(0).Enabled = true;
                UIManager.Instance.GetWidget<TokenWidget>(0).Enabled = true;
                UIManager.Instance.GetWidget<DiamondsWidget>(0).Enabled = true;
                SetDoorState(true);
            });
        }
        private Tween SetDoorState(bool state)
        {
            var localOffset = state ? doorOffset : -doorOffset;
            leftDoor.DOMoveX(leftDoor.position.x + localOffset, 1);
            var t = rightDoor.DOMoveX(rightDoor.position.x - localOffset, 1);
            return t;
        }
    }
}