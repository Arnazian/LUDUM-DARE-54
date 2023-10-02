using System;

namespace Cards
{
    public class Meditation : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);

        public override string Name => "Meditation";
        public override string Description => "Decrease Cooldown of another card by 5";

        public override Type[] Selections => new Type[] { typeof(AbstractCard) };

        public override void OnPlayed(params object[] args)
        {
            Combat.Player.DoHealing(2);
            foreach (var card in args) (card as AbstractCard).Cooldown.Value -= 5;
        }
    }
}