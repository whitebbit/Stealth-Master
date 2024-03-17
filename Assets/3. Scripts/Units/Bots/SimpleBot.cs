using _3._Scripts.FSM.Base;
using _3._Scripts.Marks;
using _3._Scripts.Marks.Enums;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot : Bot
    {
        [SerializeField] private UnitNavMeshAgent unitNavMeshAgent;

        [Header("FSM")] [SerializeField] private SimpleBotFSM simpleBotFsm;

        [Header("Visual")] [SerializeField] private MarkSystem markSystem;
        [SerializeField] private MeshRenderer meshRenderer;
        protected override void InitializeFSM()
        {
            simpleBotFsm.Initialize(Fsm, unitNavMeshAgent);
            Fsm.StateMachine.SetState(simpleBotFsm.GetState<PatrolState>());

            SubscribeToStates();
        }
        
        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            simpleBotFsm.SetCurrentVisitor(visitor);
            simpleBotFsm.SetCurrentVisitorPosition(visitor);

            if (Fsm.StateMachine.CurrentState == simpleBotFsm.GetState<AttackState>()) simpleBotFsm.Chasing = true;
            if (Fsm.StateMachine.CurrentState == simpleBotFsm.GetState<SearchState>()) simpleBotFsm.Chasing = false;
        }
        
        private void SubscribeToStates()
        {
            simpleBotFsm.GetState<ChaseState>().OnEnterAction += () => QuestionMarkState(true);
            simpleBotFsm.GetState<ChaseState>().OnExitAction += () => QuestionMarkState(false);
            
            simpleBotFsm.GetState<SearchState>().OnEnterAction += () => QuestionMarkState(true);
            simpleBotFsm.GetState<SearchState>().OnExitAction += () => QuestionMarkState(false);
            
            simpleBotFsm.GetState<AttackState>().OnEnterAction += () => OnAttack(true);
            simpleBotFsm.GetState<AttackState>().OnExitAction += () => OnAttack(false);
        }
        
        private void QuestionMarkState(bool state)
        {
            markSystem.SetMark(state ? MarkType.Question : MarkType.None);
        }
        
        private void OnAttack(bool state)
        {
            var color = state ? Color.red : Color.white;
            var sprite = state ? MarkType.Explanation : MarkType.None;
            
            color.a = meshRenderer.materials[0].color.a;
            markSystem.SetMark(sprite);
            meshRenderer.material.DOColor(color, 0.25f);
        }

        public override void HearingNoise(Vector3 soundPosition)
        {
            Debug.Log("HearingNoise");
            var s = simpleBotFsm.GetState<SearchState>();
            simpleBotFsm.Searching = true;
            s.GoToSearchPoint = true;
            s.SearchPoint = soundPosition;
        }
    }
}