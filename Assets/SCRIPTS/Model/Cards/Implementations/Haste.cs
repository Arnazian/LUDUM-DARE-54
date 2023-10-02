namespace Cards
{
    public class Haste : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override string Name => "Haste";
        public override string Description => "Reduce Cooldown of all cards by 1";

        public override void OnPlayed(params object[] args)
        {
            var player = Combat.Player;
            foreach (var card in player.Cards)
                card.Cooldown.Value -= 1;
        }
    }
}