using System;
using _3._Scripts.Detectors;
using _3._Scripts.FSM.Base;
using _3._Scripts.Noises;
using _3._Scripts.Noises.Interfaces;
using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using _3._Scripts.Units.Player;
using _3._Scripts.Units.Utils;
using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.AI;

namespace _3._Scripts.Units.Bots
{
    public abstract class Bot : Unit, INoiseListener
    {
        [Header("Bot Settings")] [SerializeField]
        protected BaseDetector<IWeaponVisitor> detector;

        private UnitAnimator unitAnimator;
        private NavMeshAgent navMeshAgent;
        private AimIK aimIK;
        private Ragdoll ragdoll;

        protected readonly FSMHandler Fsm = new();

        public override void Dead()
        {
            NoiseEmitter.MakeNoise(transform.position, gameObject.layer, 20);
            ragdoll.State = true;
        }

        protected override void OnAwake()
        {
            InitializeComponents();
        }

        protected override void OnStart()
        {
            ragdoll.onStateChanged += ChangeStateByRagdoll;
            detector.OnFound += OnDetectorFind;
            InitializeFSM();
        }

        protected abstract void InitializeFSM();

        protected abstract void OnDetectorFind(IWeaponVisitor visitor);

        private void InitializeComponents()
        {
            ragdoll = GetComponent<Ragdoll>();
            unitAnimator = GetComponent<UnitAnimator>();
            aimIK = GetComponent<AimIK>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(Health.Health > 0)
                Fsm.StateMachine.Update();
        }

        private void ChangeStateByRagdoll(bool state)
        {
            unitAnimator.SetState(!state);
            aimIK.enabled = !state;
            navMeshAgent.enabled = !state;
        }

        public virtual void HearingNoise(Vector3 soundPosition)
        {
        }
    }
}