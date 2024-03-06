using _3._Scripts.Heroes.Perks;
using _3._Scripts.Units.Player;

namespace _3._Scripts.Heroes
{
    public class NinjaHero: Hero
    {
        protected override void InitializePerks()
        {
            FirstPerk = new SpeedPerk(GetComponent<PlayerMovement>(), 10);
            SecondPerk = new SilentPerk();
        }
    }
}