using _3._Scripts.Controls.Interfaces;
using UnityEngine;

namespace _3._Scripts.Controls
{
    public class JoystickInput: IInput
    {
        private readonly Joystick joystick;

        public JoystickInput(Joystick joystick)
        {
            Input.multiTouchEnabled = false;
            this.joystick = joystick;
        }
        
        public bool Active()
        {
            return joystick.Direction != Vector2.zero;
        }

        public Vector3 Direction()
        {
            return joystick.Direction;
        }
    }
}