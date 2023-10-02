using System;



namespace Cards
{
    public class Deathmark : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 1);
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Deathmark";
        public override string Description => "Apply Mark of Death.<br>Target takes 1 extra damage from all sources.";

        public override void OnPlayed(params object[] args)
        {
            foreach (var target in args) (target as IStatusEffectTarget)?.Apply<Deathmarked>(1, blending: IStatusEffectTarget.ApplyBlending.Max);
        }
    }
}