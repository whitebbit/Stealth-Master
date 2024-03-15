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

            SetFSMTransition();

            Fsm.StateMachine.SetState(patrolState);
        }

        private void SetFSMTransition()
        {
            Fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected && !chasing && !searching));
            Fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            Fsm.AddTransition(chaseState, new FuncPredicate(() => chasing && !attackState.Attacking));
            Fsm.AddTransition(chaseState, searchState, new FuncPredicate(() => !visitorDetected && searching));
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
            SetCurrentVisitor(visitor);
            SetCurrentVisitorPosition(visitor);
            
            if (Fsm.StateMachine.CurrentState == attackState) chasing = true;
            if (Fsm.StateMachine.CurrentState == searchState) chasing = false;
        }

        private void SetCurrentVisitorPosition(IWeaponVisitor visitor)
        {
            if (visitor != default)
                chaseState.LastVisitor = visitor;
        }

        private void SetCurrentVisitor(IWeaponVisitor visitor)
        {
            attackState.LastVisitor = visitor;
            visitorDetected = visitor != default;
        }
    }
}