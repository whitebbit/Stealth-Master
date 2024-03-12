using System;
using _3._Scripts.Controls;
using _3._Scripts.Controls.Interfaces;
using _3._Scripts.Controls.Scriptable;
using _3._Scripts.Units.Animations;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MovementConfig config;
        [SerializeField] private Joystick joystick;


        public float SpeedMultiplier { get; set; }
        private Rigidbody rb;
        private IMovable movable;
        private UnitAnimator animator;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<UnitAnimator>();
            movable = new InputMovable(new JoystickInput(joystick));
        }

        private void Start()
        {
            SpeedMultiplier = 1;
        }

        private void OnEnable()
        {
            movable.Moved += Move;
            movable.Stopped += Stop;
        }

        private void Update()
        {
            movable.Move();
        }

        private void OnDisable()
        {
            movable.Moved -= Move;
            movable.Stopped -= Stop;
        }

        private void Move(Vector3 direction)
        {
            var lookRotation = Quaternion.LookRotation(direction);
            var speed = config.MoveSpeed * (1 + SpeedMultiplier / 100);
            rb.transform.rotation =
                Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * config.RotationSpeed);
            rb.MovePosition(rb.position + transform.forward * direction.magnitude * speed * Time.deltaTime);
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