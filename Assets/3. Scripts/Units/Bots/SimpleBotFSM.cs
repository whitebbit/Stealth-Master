using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using UnityEngine;

namespace _3._Scripts.Units.Bots
{
    [Serializable]
    public class SimpleBotFSM
    {
        [SerializeField] private PatrolState patrolState;
        [SerializeField] private AttackState attackState;
        [SerializeField] private ChaseState chaseState;
        [SerializeField] private SearchState searchState;

        private bool visitorDetected;
        public bool Chasing { get; set; }
        public bool Searching { get; set; }

        public void Initialize(FSMHandler fsm, UnitNavMeshAgent unitNavMeshAgent)
        {
            SetFSMTransition(fsm);
            SubscribeToStates();
            SetUnitAgentToStates(unitNavMeshAgent);
            patrolState.StartPosition = unitNavMeshAgent.Agent.transform.position;
        }

        public void SetCurrentVisitorPosition(IWeaponVisitor visitor)
        {
            if (visitor != default)
                chaseState.LastVisitor = visitor;
        }

        public void SetCurrentVisitor(IWeaponVisitor visitor)
        {
            attackState.LastVisitor = visitor;
            visitorDetected = visitor != default;
        }

        public T GetState<T>()
        {
            if (typeof(T) == typeof(AttackState))return (T)(object)attackState;
            if (typeof(T) == typeof(PatrolState))return (T)(object)patrolState;
            if (typeof(T) == typeof(ChaseState))return (T)(object)chaseState;
            if (typeof(T) == typeof(SearchState))return (T)(object)searchState;
    
            return default;
        }

        private void SetFSMTransition(FSMHandler fsm)
        {
            fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected && !Chasing && !Searching));
            fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            fsm.AddTransition(chaseState, new FuncPredicate(() => Chasing && !attackState.Attacking));
            fsm.AddTransition(searchState, new FuncPredicate(() => !visitorDetected && Searching));
        }

        private void SubscribeToStates()
        {
            chaseState.OnChasingFinish += ChaseStateOnChasingFinish;
            searchState.OnSearchEnd += SearchStateOnSearchEnd;
        }

        private void SetUnitAgentToStates(UnitNavMeshAgent unitNavMeshAgent)
        {
            patrolState.UnitAgent = unitNavMeshAgent;
            chaseState.UnitAgent = unitNavMeshAgent;
            searchState.UnitAgent = unitNavMeshAgent;
        }

        private void SearchStateOnSearchEnd()
        {
            Chasing = false;
            Searching = false;
        }

        private void ChaseStateOnChasingFinish()
        {
            Chasing = false;
            Searching = true;
        }
    }
}