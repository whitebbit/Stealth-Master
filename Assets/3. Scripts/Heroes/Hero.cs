using System;
using _3._Scripts.Heroes.Perks;
using _3._Scripts.Heroes.Scriptable;
using _3._Scripts.Units.Player;
using UnityEngine;

namespace _3._Scripts.Heroes
{
    public abstract class Hero: MonoBehaviour
    {
        [SerializeField] protected HeroData data;
        
        protected Perk FirstPerk; 
        protected Perk SecondPerk;
        
        private Player player;
        private SkinnedMeshRenderer meshRenderer;
        private void Awake()
        {
            player = GetComponent<Player>();
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        private void Start()
        {
            player.Health.SetMaxHealth(data.StartHealth);
            
            InitializePerks();
            ActivatePerks();
        }

        protected abstract void InitializePerks();

        private void ActivatePerks()
        {
            if(true)
                FirstPerk.Activate();
            if(0 < 9)
                SecondPerk.Activate();
        }
    }
}