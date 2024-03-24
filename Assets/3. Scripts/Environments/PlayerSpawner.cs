using System;
using System.Collections.Generic;
using System.Linq;
using _3._Scripts.CameraControllers;
using _3._Scripts.CameraControllers.Enums;
using _3._Scripts.Heroes;
using _3._Scripts.Heroes.Scriptable;
using _3._Scripts.Units.Player;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

namespace _3._Scripts.Environments
{
    public class PlayerSpawner: MonoBehaviour
    { 
        [SerializeField] private List<HeroData> data;
        [SerializeField] private Transform parent;

        [Header("Components")] [SerializeField]
        private Joystick joystick;
        private void Awake()
        {
            var id = YandexGame.savesData.playerSave.heroes.currentHero;
            var obj = data.FirstOrDefault(d => d.ID == id)?.Prefab;
            var hero = Instantiate(obj, parent);
            var movement = hero.GetComponent<PlayerMovement>();
            var heroTransform = hero.transform;
            
            movement.SetJoystick(joystick);
            heroTransform.localPosition = Vector3.zero;
            
            PlayerCameraController.Instance.SetTarget(PlayerCameraMode.Play, heroTransform);
            PlayerCameraController.Instance.SetTarget(PlayerCameraMode.Start, heroTransform);
        }
    }
}