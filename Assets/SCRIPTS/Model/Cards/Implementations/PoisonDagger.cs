
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class PoisonDagger : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);
        private const int Damage = 1;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Laced Dagger";
        public override string Description => "Deal 1 damage. Apply 2 Poisoned.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(Damage, args.Select(arg => arg as IDamageable).ToArray());
            foreach (var target in args) (target as IStatusEffectTarget)?.Apply<Poisoned>(2);

        }
    }
}