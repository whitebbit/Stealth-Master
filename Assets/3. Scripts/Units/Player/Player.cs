using _3._Scripts.Units.Animations;
using _3._Scripts.Units.Health;
using _3._Scripts.Units.Utils;
using RootMotion.FinalIK;

namespace _3._Scripts.Units.Player
{
    public class Player : Unit
    {
        private UnitAnimator unitAnimator;
        private AimIK aimIK;
        private PlayerMovement playerMovement;
        public override void Dead()
        {
            
        }

        protected override void OnAwake()
        {
            var ragdoll = GetComponent<Ragdoll>();
            unitAnimator = GetComponent<UnitAnimator>();
            aimIK = GetComponent<AimIK>();
            playerMovement = GetComponent<PlayerMovement>();
            ragdoll.onStateChanged += ChangeStateByRagdoll;
        }

        private void ChangeStateByRagdoll(bool state)
        {
            unitAnimator.SetState(!state);
            aimIK.enabled = !state;
            playerMovement.enabled = !state;
        }
    }
}