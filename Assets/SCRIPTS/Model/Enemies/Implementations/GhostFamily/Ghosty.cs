using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosty : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(2, 2);
    protected override CappedInt Health { get; set; } = new(3, 3);
    public override string PrefabName => "Ghosty";
    public override int Damage => 1;
    public Ghosty()
    {
        EffectTarget.Apply<PhasedOut>(-1);
    }

    public override void Act()
    {
        DealDamage();
    }
}
