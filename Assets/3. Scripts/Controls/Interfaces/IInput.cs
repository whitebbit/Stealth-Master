

using UnityEngine;

namespace _3._Scripts.Controls.Interfaces
{
    public interface IInput
    {
        public bool Active();
        public Vector3 Direction();
    }
}