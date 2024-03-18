using System;
using _3._Scripts.Units.Player;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Environments
{
    public class ElevatorEnd : MonoBehaviour
    {
        [SerializeField] private Transform finishPoint;
        [Space] [SerializeField] private Transform rightDoor;
        [SerializeField] private Transform leftDoor;
        [Space] [SerializeField, Min(0)] private float doorOffset = 1.75f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player playerMovement))
                DoFinish(playerMovement);
        }

        private void DoFinish(Player target)
        {
            SetDoorState(true);
            target.GoToFinishPoint(finishPoint, 1, () => SetDoorState(false))
                .SetDelay(0.25f);
        }

        private void SetDoorState(bool state)
        {
            var localOffset = state ? doorOffset : -doorOffset;
            leftDoor.DOMoveX(leftDoor.position.x + localOffset, 1);
            rightDoor.DOMoveX(rightDoor.position.x - localOffset, 1);
        }
    }
}