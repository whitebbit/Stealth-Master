using System;
using UnityEngine;

namespace _3._Scripts.Levels
{
    public class AlarmLight : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;


        private void OnEnable()
        {
            Level.Instance.OnAlarm += LightState;
        }
        
        
        private void OnDisable()
        {
            Level.Instance.OnAlarm -= LightState;
        }
        
        private void LightState(bool state)
        {
            Debug.Log(state);
            particle.gameObject.SetActive(state);
            if (state) particle.Play();
            else particle.Stop();
        }
    }
}