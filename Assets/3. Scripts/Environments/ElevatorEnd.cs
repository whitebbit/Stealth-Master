using System;
using _3._Scripts.CameraControllers;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.Units.Player;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _3._Scripts.Environments
{
    public class ElevatorEnd : MonoBehaviour
    {
        [SerializeField] private Transform finishPoint;
        [Space] [SerializeField, Min(0)] private float doorOffset = 1.75f;
        [Space] [SerializeField] private Transform cabin;
        [Space] [SerializeField] private Transform rightDoor;
        [SerializeField] private Transform leftDoor;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player playerMovement))
                DoFinish(playerMovement);
        }

        private void DoFinish(Player target)
        {
            SetDoorState(true);
            PlayerCameraController.Instance.SetState(PlayerCameraMode.Finish);
            target.GoToFinishPoint(finishPoint, 1, () => SetDoorState(false).OnComplete(() =>
                {
                    target.transform.parent = cabin;
                    cabin.DOMoveY(cabin.transform.position.y + 15, 2).OnComplete(() =>
                    {
                        SceneManager.LoadScene(0);
                    }).SetLink(cabin.gameObject);
                }))
                .SetDelay(0.25f);
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