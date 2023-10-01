
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Shield : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override string Name => "Block";
        public override string Description => "Gain 5 Block.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.StatusEffectTarget.Apply<Block>(5);
        }
    }
}