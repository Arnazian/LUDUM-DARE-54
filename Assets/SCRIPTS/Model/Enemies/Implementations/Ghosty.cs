using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghosty : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(1, 1);
    protected override CappedInt Health { get; set; } = new(4, 4);
    public override string PrefabName => "Ghosty";
    public override void Act()
    {
        DealDamage(1);
    }
}
