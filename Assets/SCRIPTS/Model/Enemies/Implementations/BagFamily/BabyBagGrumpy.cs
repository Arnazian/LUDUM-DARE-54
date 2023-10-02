using UnityEngine;

public class BabyBagGrumpy : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(4, 4);
    protected override CappedInt Health { get; set; } = new(10, 10);
    public override string PrefabName => "BabyBag";

    public BabyBagGrumpy()
    {
        EffectTarget.Apply<Determined>(-1);
    }

    public override void Act()
    {
        DealDamage(2);
    }

    public class Determined : AbstractStatusEffect
    {
        public override Color Color => new Color(.7f, 1f, 1f);
        public override string Name => "Determined";
        public override string Description => "Takes half damage.";

        public override void OnBeforeRecieveDamage(ref int amount)
        {
            amount = (amount + 1) / 2;
        }
    }
}
