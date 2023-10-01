using System.Collections.Generic;

namespace Cards
{
    public class CardName : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override void OnPlayed(params object[] args)
        {
            var player = Combat.Player;
        }
    }
}