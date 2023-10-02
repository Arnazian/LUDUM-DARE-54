using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBagQueen : AbstractEnemy
{
    protected override CappedInt ActCooldown { get; set; } = new(2, 2);
    protected override CappedInt Health { get; set; } = new(10, 10);
    public override void Act()
    {
        DealDamage(5);
    }
}
