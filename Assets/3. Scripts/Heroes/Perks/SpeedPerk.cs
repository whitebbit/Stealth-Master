using _3._Scripts.Units.Player;

namespace _3._Scripts.Heroes.Perks
{
    public class SpeedPerk: Perk
    {
        private readonly PlayerMovement movement;
        private readonly float percentValue;
        
        public SpeedPerk(PlayerMovement movement, float percentValue)
        {
            this.movement = movement;
            this.percentValue = percentValue;
        }
        
        public override void Activate()
        {
            movement.SpeedMultiplier = percentValue;
        }
    }
}