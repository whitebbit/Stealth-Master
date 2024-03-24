using _3._Scripts.CameraControllers;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.Environments;
using _3._Scripts.LevelManager;
using _3._Scripts.UI.Panels;
using _3._Scripts.UI.Widgets;
using UnityEngine;
using UnityEngine.EventSystems;
using Stage = _3._Scripts.Environments.Stage;

namespace _3._Scripts.UI.Utils
{
    public class StartButton: MonoBehaviour, IPointerDownHandler
    {
        private bool started;

        public void OnPointerDown(PointerEventData eventData)
        {
            if(started) return;
            
            PlayerCameraController.Instance.SetState(PlayerCameraMode.Play);
            
            UIManager.Instance.GetWidget<DiamondsWidget>().Enabled = false;
            UIManager.Instance.GetWidget<TokenWidget>().Enabled = false;
            UIManager.Instance.GetPanel<LevelBarPanel>().Enabled = false;
            UIManager.Instance.GetPanel<MainPanel>().Enabled = false;

            if (LevelHandler.CurrentStageIndex == 0)
            {
                UIManager.Instance.GetPanel<AmmunitionRoulettePanel>().Enabled = true;
                UIManager.Instance.GetWidget<FreeMoneyWidget>().Enabled = true;
                UIManager.Instance.GetPanel<JoystickPanel>().Enabled = false;
            }
            else
            {
                Stage.Instance.OnPlay = true;
                UIManager.Instance.GetPanel<UnitCounterPanel>().Enabled = true;
            }
            
            started = true;
        }
    }
}