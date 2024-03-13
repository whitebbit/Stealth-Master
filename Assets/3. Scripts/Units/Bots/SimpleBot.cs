using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using UnityEngine;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot: Bot
    {
        [Header("FSM")] [SerializeField] private PatrolState patrolState;
        protected override void InitializeFSM()
        {
            Fsm.AddTransition(patrolState, new FuncPredicate(() => true));
            Fsm.StateMachine.SetState(patrolState);
        }
    }
}