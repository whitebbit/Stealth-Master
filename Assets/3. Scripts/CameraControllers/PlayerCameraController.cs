using _3._Scripts.Singleton;
using Cinemachine;
using UnityEngine;

namespace _3._Scripts.CameraControllers
{
    public class PlayerCameraController: Singleton<PlayerCameraController>
    {
        [SerializeField] private CinemachineVirtualCamera startCamera;
        [SerializeField] private CinemachineVirtualCamera playCamera;

        public void SetPlayState(bool state)
        {
            var playPriority = state ? 10 : 0;
            var startPriority = state ? 0 : 10;

            startCamera.Priority = startPriority;
            playCamera.Priority = playPriority;
        }

        public void DisableFollowing()
        {
            playCamera.Follow = null;
            playCamera.LookAt = null;
        }
    }
}