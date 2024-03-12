using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Player;
using _3._Scripts.Units.Utils;
using RootMotion.FinalIK;

namespace _3._Scripts.Units.Bots
{
    public class Bot: Unit
    {
        private UnitAnimator unitAnimator;
        private AimIK aimIK;
        private Ragdoll ragdoll;
        protected override void OnAwake()
        {
            ragdoll = GetComponent<Ragdoll>();
            unitAnimator = GetComponent<UnitAnimator>();
            aimIK = GetComponent<AimIK>();
            ragdoll.onStateChanged += ChangeStateByRagdoll;
        }

        public override void Dead()
        {
            ragdoll.State = true;
        }

        private void ChangeStateByRagdoll(bool state)
        {
            unitAnimator.SetState(!state);
            aimIK.enabled = !state;
        }
    }
}