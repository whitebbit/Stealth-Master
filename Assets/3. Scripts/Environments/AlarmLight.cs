using UnityEngine;

namespace _3._Scripts.Environments
{
    public class AlarmLight : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        
        private void Start()
        {
            Environment.Instance.OnAlarm += LightState;
        }
        
        private void LightState(bool state)
        {
            particle.gameObject.SetActive(state);
            if (state) particle.Play();
            else particle.Stop();
        }
    }
}