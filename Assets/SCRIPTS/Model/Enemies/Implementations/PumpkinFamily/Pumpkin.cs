using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(12, 12);
    protected override CappedInt Health { get; set; } = new(8, 8);
    public override string PrefabName => "Pumpkin";

    public Pumpkin()
    {
        this.EffectTarget.Apply<Vengeful>(-1);
    }

    public override void Act()
    {
        DealDamage(2);
    }

    public class Vengeful : AbstractStatusEffect
    {
        public override Color Color => new Color(.8f, .4f, .4f);
        public override string Name => "Vengeful";
        public override string Description => "Reduce action timer upon taking damage.";

        public override void OnBeforeRecieveDamage(ref int amount)
        {
            if (EffectTarget is AbstractEnemy) ((AbstractEnemy)EffectTarget).ActCooldown.Value -= 1;
        }
    }
}
