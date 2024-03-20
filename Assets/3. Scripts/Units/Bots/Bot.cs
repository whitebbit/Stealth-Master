using System;
using System.Collections;
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
using Environment = _3._Scripts.Environments.Environment;

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
            var position = transform.position;
            var position1 = LastDamageDealer.position;

            NoiseEmitter.MakeNoise(position, gameObject.layer);
            gameObject.layer = LayerMask.NameToLayer("PlayerIgnore");
            detector.gameObject.SetActive(false);

            ragdoll.State = true;
            ragdoll.AddForce(0, (position - position1).normalized + Vector3.up * 1.25f
                , 5000);


            OnDead();
        }

        protected override void OnAwake()
        {
            InitializeComponents();
        }

        protected override void OnStart()
        {
            ragdoll.OnStateChanged += ChangeStateByRagdoll;
            detector.OnFound += OnDetectorFind;
            Environment.Instance.AddBot(this);
            InitializeFSM();
        }

        protected abstract void InitializeFSM();

        protected abstract void OnDetectorFind(IWeaponVisitor visitor);


        protected virtual void OnDead()
        {
        }

        private void InitializeComponents()
        {
            ragdoll = GetComponent<Ragdoll>();
            unitAnimator = GetComponent<UnitAnimator>();
            aimIK = GetComponent<AimIK>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (Health.Health > 0)
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

        private static bool CheckDeadBot(Bot bot)
        {
            return bot.Health.Health <= 0 && Environment.Instance.ContainsBot(bot);
        }

        protected IEnumerator EnableAlarm(Bot bot, float delay = 1, Action action = null)
        {
            if (!CheckDeadBot(bot)) yield break;

            action?.Invoke();
            Environment.Instance.RemoveBot(bot);

            yield return new WaitForSeconds(delay);
            if (Health.Health <= 0) yield break;
            Environment.Instance.Alarm = true;
        }
    }
}