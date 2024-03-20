using UnityEngine;

namespace _3._Scripts.Units.HitBoxes
{
    public class DefaultHitBox: HitBox
    {
        [SerializeField] private Transform aimTarget;
        public override void Visit(float damage, Transform dealer = null)
        {
            unit.LastDamageDealer = dealer;
            unit.ApplyDamage(damage);
        }

        public override Transform Target()
        {
            return aimTarget;
        }
    }
}