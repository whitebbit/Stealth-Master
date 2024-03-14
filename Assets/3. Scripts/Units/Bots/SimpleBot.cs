using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot: Bot
    {
        [Header("FSM")] [SerializeField] private PatrolState patrolState;
        [SerializeField] private AttackState attackState;

        private bool visitorDetected;
        private Vector3 visitorLastPosition;
        protected override void InitializeFSM()
        {
            Fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected));
            Fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            Fsm.StateMachine.SetState(patrolState);
        }

        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            attackState.LastVisitor = visitor;
            visitorDetected = visitor != default;
            
            if(visitor == default) return;
            
            visitorLastPosition = visitor.Transform().position;
        }
    }
}