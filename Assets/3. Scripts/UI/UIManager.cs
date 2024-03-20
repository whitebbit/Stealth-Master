using System;
using System.Collections;
using _3._Scripts.UI.Panels;
using UnityEngine;

namespace _3._Scripts.UI
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private UIScreen startScreen;

        private void Awake()
        {
            startScreen.Initialize();
        }

        private void Start()
        {
            startScreen.Open();
            StartCoroutine(Test());
        }

        private IEnumerator Test()
        {
            while (true)
            {
                yield return new WaitForSeconds(2);
                startScreen.GetPanel<MainBottomPanel>().Opened = false;
                yield return new WaitForSeconds(2);
                startScreen.GetPanel<MainBottomPanel>().Opened = true;
            }
        }
        
    }
}