using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(2, 2);
    protected override CappedInt Health { get; set; } = new(5, 5);
    public override string PrefabName => "Pumpkin";

    public override void Act()
    {
        DealDamage(5);
    }
}
