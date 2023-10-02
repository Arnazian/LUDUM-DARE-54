
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class BloodRitual : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Blood Ritual";
        public override string Description => "Suffer 2 damage to deal 10.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.RecieveDamage(2);
            GameSession.Player.DoDamage(10, args.Select(arg => arg as IDamageable).ToArray());
        }
    }
}