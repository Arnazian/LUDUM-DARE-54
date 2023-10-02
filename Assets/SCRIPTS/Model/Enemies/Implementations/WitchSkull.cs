using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSkull : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(8, 8);
    protected override CappedInt Health { get; set; } = new(20, 20);
    public override string PrefabName => "WitchSkull";

    public override void Act()
    {
        DealDamage(15);
    }
}
