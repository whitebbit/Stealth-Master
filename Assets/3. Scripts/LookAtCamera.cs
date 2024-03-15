using UnityEngine;

namespace _3._Scripts
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform target; 

        private void Start()
        {
            if (Camera.main != null) target = Camera.main.transform; 
        }

        private void Update()
        {
            transform.LookAt(target);
        }
    }
}