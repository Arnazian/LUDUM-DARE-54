
using System;
using UnityEngine;


namespace Cards
{
    public class Fireball : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);
        private const int Damage = 3;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Dagger";
        public override string Description => "Deal 5 Damage.";

        public override void OnPlayed(params object[] args)
        {
            foreach (var target in args)
            {
                (target as IDamageable)?.RecieveDamage(Damage);                
            }
        }
    }
}