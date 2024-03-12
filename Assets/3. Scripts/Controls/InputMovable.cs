using System;
using _3._Scripts.Controls.Interfaces;
using UnityEngine;

namespace _3._Scripts.Controls
{
    public class InputMovable : IMovable
    {
        public event Action<Vector3> Moved;
        public event Action Stopped;

        private readonly IInput input;

        public InputMovable(IInput input)
        {
            this.input = input;
        }

        public void Move()
        {
            if (!input.Active())
            {
                Stopped?.Invoke();
                return;
            }

            var direction = new Vector3(input.Direction().x, 0, input.Direction().y);
            Moved?.Invoke(direction);
        }
    }
}