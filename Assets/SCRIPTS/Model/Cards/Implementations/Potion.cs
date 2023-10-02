using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Potion : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 0);

        
        public override string Name => "Potion";
        public override string Description => "Fully heal.<br>Consumed on use.";

        public override void OnPlayed(params object[] args)
        {
            var player = Combat.Player;
            player.DoHealing(player.ReadOnlyHealth.Max, player);
            player.RemoveCard(this);
        }
    }
}