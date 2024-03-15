using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot : Bot
    {
        [SerializeField] private UnitNavMeshAgent unitNavMeshAgent;

        [Header("FSM")]
        [SerializeField] private SimpleBotFSM simpleBotFsm;

        protected override void InitializeFSM()
        {
            simpleBotFsm.Initialize(Fsm, unitNavMeshAgent);
            Fsm.StateMachine.SetState(simpleBotFsm.GetState<PatrolState>());
        }
        

        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            simpleBotFsm.SetCurrentVisitor(visitor);
            simpleBotFsm.SetCurrentVisitorPosition(visitor);

            if (Fsm.StateMachine.CurrentState == simpleBotFsm.GetState<AttackState>()) simpleBotFsm.Chasing = true;
            if (Fsm.StateMachine.CurrentState == simpleBotFsm.GetState<SearchState>()) simpleBotFsm.Chasing  = false;
        }

    }
}