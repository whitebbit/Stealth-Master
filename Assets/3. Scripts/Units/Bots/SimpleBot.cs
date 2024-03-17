using _3._Scripts.Marks;
using _3._Scripts.Marks.Enums;
using _3._Scripts.Units.Bots.States;
using _3._Scripts.Units.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _3._Scripts.Units.Bots
{
    public class SimpleBot : Bot
    {
        [SerializeField] private UnitNavMeshAgent unitNavMeshAgent;

        [Header("FSM")] [SerializeField] private SimpleBotFSM simpleBotFsm;
        [SerializeField] private MarkSystem markSystem;
        [SerializeField] private MeshRenderer meshRenderer;

        protected override void InitializeFSM()
        {
            simpleBotFsm.Initialize(Fsm, unitNavMeshAgent);
            Fsm.StateMachine.SetState(simpleBotFsm.GetState<PatrolState>());

            SubscribeToStates();
        }

        protected override void OnDead()
        {
            markSystem.SetMark(MarkType.None);
        }

        protected override void OnDetectorFind(IWeaponVisitor visitor)
        {
            if (visitor == default)
            {
                TryFindVisitor(null);
                return;
            }

            if (!visitor.Object().TryGetComponent(out Bot bot)) TryFindVisitor(visitor);
            else StartCoroutine(EnableAlarm(bot, 1, () =>
            {
                markSystem.SetMark(MarkType.Explanation);
                unitNavMeshAgent.StopMoving();
            }));
        }

        private void TryFindVisitor(IWeaponVisitor visitor)
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

            simpleBotFsm.GetState<AlarmState>().OnEnterAction += () => QuestionMarkState(true);
            simpleBotFsm.GetState<AlarmState>().OnExitAction += () => QuestionMarkState(false);
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
            var s = simpleBotFsm.GetState<SearchState>();

            simpleBotFsm.Searching = true;
            s.GoToSearchPoint = true;
            s.SearchPoint = soundPosition;
        }
    }
}