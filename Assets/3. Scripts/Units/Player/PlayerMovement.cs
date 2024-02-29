using _3._Scripts.Controls;
using _3._Scripts.Controls.Interfaces;
using _3._Scripts.Controls.Scriptable;
using UnityEngine;

namespace _3._Scripts.Units.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private MovementConfig config;
        [SerializeField] private Joystick joystick;
        [Space] [SerializeField] private Transform rotateObject;

        private Rigidbody rb;
        private IMovable movable;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            movable = new InputMovable(new JoystickInput(joystick));
        }

        private void Start()
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
            var targetRotation = Quaternion.LookRotation(direction);

            rotateObject.rotation = Quaternion.Lerp(rotateObject.rotation, targetRotation,
                config.RotationSpeed * Time.deltaTime);
            rb.velocity = direction * config.MoveSpeed;
        }

        private void Stop()
        {
            if (rb != null)
                rb.velocity = Vector3.zero;
        }
    }
}