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
}
