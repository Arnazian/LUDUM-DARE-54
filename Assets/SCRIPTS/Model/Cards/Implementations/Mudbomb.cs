using System;

namespace Cards
{
    public class Mudbomb : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);

        public override string Name => "Mudbomb";
        public override string Description => "Delay enemy actions for 2 turns.";

        public override void OnPlayed(params object[] args)
        {
            foreach (var enemy in Combat.Active.Enemies) enemy.ActCooldown.Value += 2;
        }
    }
}