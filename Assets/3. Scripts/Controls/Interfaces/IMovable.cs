using System;
using UnityEngine;

namespace _3._Scripts.Controls.Interfaces
{
    public interface IMovable
    {
        public void Move();
        public event Action<Vector3> Moved;
        public event Action Stopped;
    }
}