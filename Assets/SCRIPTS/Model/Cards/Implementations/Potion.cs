using System.Collections.Generic;

namespace Cards
{
    public class Potion : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override void OnPlayed(List<AbstractEnemy> targets)
        {
            var player = Combat.ActiveCombat.Player;
            player.Health.Value = player.Health.Max;
        }
    }
}