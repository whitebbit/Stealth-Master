﻿namespace _3._Scripts.Units.HitBoxes
{
    public class DefaultHitBox: HitBox
    {
        public override void Visit(float damage)
        {
            unit.ApplyDamage(damage);
        }
    }
}