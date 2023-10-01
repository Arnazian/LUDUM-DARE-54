
using System;


namespace Cards
{
    public class Dagger : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);
        private const int Damage = 5;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };
        public override void OnPlayed(params object[] args)
        {
            foreach (var target in args)
                (target as AbstractEnemy)?.Damage(Damage);
        }
    }
}