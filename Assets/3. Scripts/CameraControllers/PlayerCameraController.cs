using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.Singleton;
using Cinemachine;
using UnityEngine;

namespace _3._Scripts.CameraControllers
{
    public class PlayerCameraController: Singleton<PlayerCameraController>
    {
        [SerializeField] private List<PlayerCamera> cameras = new ();

        private void Start()
        {
            SetState(PlayerCameraMode.Start);
        }

        public void SetTarget(PlayerCameraMode mode,Transform target)
        {
            var playerCamera = cameras.FirstOrDefault(c => c.Mode == mode);
            playerCamera?.SetTarget(target);
        }
        
        public void SetState(PlayerCameraMode mode)
        {
            foreach (var playerCamera in cameras)
            {
                if (playerCamera.Mode == mode)
                {
                    playerCamera.SetPriority(10);
                    continue;
                }
                playerCamera.SetPriority(0);
            }
        }
        
    }
}