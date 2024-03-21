using _3._Scripts.CameraControllers;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.UI.Panels;
using _3._Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3._Scripts.UI.Utils
{
    public class StartButton: MonoBehaviour, IPointerDownHandler
    {
        private bool started;

        public void OnPointerDown(PointerEventData eventData)
        {
            if(started) return;
            
            PlayerCameraController.Instance.SetState(PlayerCameraMode.Play);
            
            UIManager.Instance.GetWidget<WalletWidget>().PlayMode = true;
            UIManager.Instance.GetPanel<JoystickPanel>().Enabled = true;
            UIManager.Instance.GetPanel<UnitCounterPanel>().Enabled = true;
            UIManager.Instance.GetPanel<LevelBarPanel>().Enabled = false;
            UIManager.Instance.GetPanel<MainPanel>().Enabled = false;
            
            started = true;

        }
    }
}