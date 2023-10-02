
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class Dagger : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 0);
        private const int Damage = 2;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Dagger";
        public override string Description => "Deal 2 damage.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(Damage, args.Select(arg => arg as IDamageable).ToArray());
        }
    }
}