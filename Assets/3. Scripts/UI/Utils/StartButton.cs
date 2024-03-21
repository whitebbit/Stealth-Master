using _3._Scripts.CameraControllers;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.UI.Panels;
using _3._Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _3._Scripts.UI.Utils
{
    public class StartButton: MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            PlayerCameraController.Instance.SetState(PlayerCameraMode.Play);
            
            UIManager.Instance.GetPanel<JoystickPanel>().Enabled = true;
            UIManager.Instance.GetPanel<LevelBarPanel>().Enabled = false;
            UIManager.Instance.GetWidget<WalletWidget>().PlayMode = true;
            UIManager.Instance.GetPanel<MainPanel>().Enabled = false;
        }
    }
}