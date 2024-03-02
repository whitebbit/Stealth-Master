using _3._Scripts.Units.Interfaces;

namespace _3._Scripts.Units.Weapons.Interfaces
{
    public interface IWeapon
    {
        public void Attack(IWeaponVisitor visitor);
    }
}