using System;
using _3._Scripts.Controls;
using _3._Scripts.Controls.Interfaces;
using _3._Scripts.Controls.Scriptable;
using _3._Scripts.Environments;
using _3._Scripts.Units.Animations;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MovementConfig config;
        //[SerializeField] private Joystick joystick;


        public float SpeedMultiplier { get; set; }
        private IMovable movable;
        private UnitAnimator animator;
        private Rigidbody rb;
        private void Awake()
        {
            animator = GetComponent<UnitAnimator>();
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            SpeedMultiplier = 1;
        }
        
        private void Update()
        {
            movable.Move();
        }

        private void OnDisable()
        {
            Stop();
            movable.Moved -= Move;
            movable.Stopped -= Stop;
        }

        public void SetJoystick(Joystick joystick)
        {
            movable = new InputMovable(new JoystickInput(joystick));
            movable.Moved += Move;
            movable.Stopped += Stop;
        }
        
        private void Move(Vector3 direction)
        {
            if(!Stage.Instance.OnPlay) return;
            if (transform.parent != null) transform.parent = null;
            
            var lookRotation = Quaternion.LookRotation(direction);
            var speed = config.MoveSpeed * (1 + SpeedMultiplier / 100);
            var t = rb.transform;

            t.rotation =
                Quaternion.Lerp(t.rotation, lookRotation, Time.deltaTime * config.RotationSpeed);
            rb.velocity = t.forward * direction.magnitude * speed;
            animator.SetFloat("Speed", direction.magnitude);
        }

        private void Stop()
        {
            if (rb != null)
                rb.velocity = Vector3.zero;
            animator.SetFloat("Speed", 0);
        }
    }
}