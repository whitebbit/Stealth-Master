using System;
using _3._Scripts.CameraControllers.Enums;
using Cinemachine;
using UnityEngine;

namespace _3._Scripts.CameraControllers
{
    [Serializable]
    public class PlayerCamera
    {
        [SerializeField] private PlayerCameraMode mode;
        [SerializeField] private CinemachineVirtualCamera camera;

        public PlayerCameraMode Mode => mode;

        public void SetPriority(int priority) => camera.Priority = priority;

        public void SetTarget(Transform target)
        {
            camera.Follow = target;
            camera.LookAt = target;
        }
    }
}