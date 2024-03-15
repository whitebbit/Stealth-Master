using System;
using _3._Scripts.FSM.Base;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using State = _3._Scripts.FSM.Base.State;

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
        private bool searching;

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

        public State GetState<T>()
        {
            if (typeof(T) == typeof(AttackState)) return attackState;
            if (typeof(T) == typeof(PatrolState)) return patrolState;
            if (typeof(T) == typeof(ChaseState)) return chaseState;
            if (typeof(T) == typeof(SearchState)) return searchState;

            return default;
        }
        
        private void SetFSMTransition(FSMHandler fsm)
        {
            fsm.AddTransition(patrolState, new FuncPredicate(() => !visitorDetected && !Chasing && !searching));
            fsm.AddTransition(attackState, new FuncPredicate(() => visitorDetected));
            fsm.AddTransition(chaseState, new FuncPredicate(() => Chasing && !attackState.Attacking));
            fsm.AddTransition(chaseState, searchState, new FuncPredicate(() => !visitorDetected && searching));
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
            searching = false;
        }

        private void ChaseStateOnChasingFinish()
        {
            Chasing = false;
            searching = true;
        }
    }
}