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
        [SerializeField] private ChaseState chaseState;

        [Header("Debug")]
        [SerializeField] private bool visitorDetected;
        [SerializeField] private bool chasing;
        protected override void InitializeFSM()
        {
            patrolState.StartPosition = transform.position;
            
            Fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected && !chasing));
            Fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            Fsm.AddTransition(chaseState, new FuncPredicate(() => chasing && !attackState.Attacking));
            
            Fsm.StateMachine.SetState(patrolState);
        }

        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            if (Fsm.StateMachine.CurrentState == patrolState)
            {
                attackState.LastVisitor = visitor;
                visitorDetected = visitor != default;
            }

            if (Fsm.StateMachine.CurrentState == attackState)
            {
                attackState.LastVisitor = visitor;
                visitorDetected = visitor != default;
                chasing = true;
                if (visitor != default) 
                    chaseState.VisitorLastPosition = visitor.Transform().position;
            }

            if (Fsm.StateMachine.CurrentState == chaseState)
            {
                chasing = visitor == default;
                visitorDetected = visitor != default;
                attackState.LastVisitor = visitor;
            }
        }
    }
}