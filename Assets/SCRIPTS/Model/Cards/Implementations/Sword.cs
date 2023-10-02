
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class Sword : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);
        private const int Damage = 3;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Short Sword";
        public override string Description => "Deal 3 damage.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(Damage, args.Select(arg => arg as IDamageable).ToArray());
        }
    }
}