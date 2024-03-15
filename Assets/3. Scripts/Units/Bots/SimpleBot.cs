using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot : Bot
    {
        [SerializeField] private UnitNavMeshAgent unitAgent;

        [Header("FSM")] [SerializeField] private PatrolState patrolState;
        [SerializeField] private AttackState attackState;
        [SerializeField] private ChaseState chaseState;
        [SerializeField] private SearchState searchState;

        [Header("Debug")] [SerializeField] private bool visitorDetected;
        [SerializeField] private bool chasing;
        [SerializeField] private bool searching;

        protected override void InitializeFSM()
        {
            SetUnitAgentToStates();

            patrolState.StartPosition = transform.position;
            chaseState.OnChasingFinish += ChaseStateOnChasingFinish;
            searchState.OnSearchEnd += SearchStateOnSearchEnd;

            Fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected && !chasing && !searching));
            Fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            Fsm.AddTransition(chaseState, new FuncPredicate(() => chasing && !attackState.Attacking));
            Fsm.AddTransition(chaseState, searchState, new FuncPredicate(() => searching));

            Fsm.StateMachine.SetState(patrolState);
        }

        private void SetUnitAgentToStates()
        {
            patrolState.UnitAgent = unitAgent;
            chaseState.UnitAgent = unitAgent;
            searchState.UnitAgent = unitAgent;
        }

        private void SearchStateOnSearchEnd()
        {
            chasing = false;
            searching = false;
        }

        private void ChaseStateOnChasingFinish()
        {
            chasing = false;
            searching = true;
        }

        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            if (Fsm.StateMachine.CurrentState == patrolState)
            {
                attackState.LastVisitor = visitor;
                visitorDetected = visitor != default;

                if (visitor != default)
                    chaseState.LastVisitor = visitor;
            }

            if (Fsm.StateMachine.CurrentState == attackState)
            {
                attackState.LastVisitor = visitor;
                visitorDetected = visitor != default;
                chasing = true;

                if (visitor != default)
                    chaseState.LastVisitor = visitor;
            }

            if (Fsm.StateMachine.CurrentState == chaseState)
            {
                visitorDetected = visitor != default;
                attackState.LastVisitor = visitor;

                if (visitor != default)
                    chaseState.LastVisitor = visitor;
            }

            if (Fsm.StateMachine.CurrentState == searchState)
            {
                chasing = false;
                visitorDetected = visitor != default;
                attackState.LastVisitor = visitor;
            }
        }
    }
}