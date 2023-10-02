
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class Greatsword : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);
        private const int Damage = 5;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Greatsword";
        public override string Description => "Deal 5 damage.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(Damage, args.Select(arg => arg as IDamageable).ToArray());
            foreach (var target in args) (target as IStatusEffectTarget)?.Apply<Bleeding>(1);
        }
    }
}