using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBag : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(4, 4);
    protected override CappedInt Health { get; set; } = new(10, 10);
    public override string PrefabName => "BabyBag";
    public override void Act()
    {
        DealDamage(5);
    }
}
