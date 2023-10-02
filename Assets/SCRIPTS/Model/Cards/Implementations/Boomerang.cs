namespace Cards
{
    public class Boomerang : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);


        public override string Name => "Boo!-merang";
        public override string Description => "Deal 2 damage. Targets all enemies.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(2, Combat.Active.Enemies.ToArray());
        }
    }
}