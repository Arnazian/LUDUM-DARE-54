using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Potion : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        
        public override string Name => "Potion";
        public override string Description => "Fully Heal. <b>Consumed</b> on use.";

        public override void OnPlayed(params object[] args)
        {
            var player = Combat.Player;
            player.Health.Value = player.Health.Max;
            player.RemoveCard(this);
        }
    }
}