using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(3, 3);
    protected override CappedInt Health { get; set; } = new(3, 3);
    public override string PrefabName => "Pumpkin";
    public override int Damage => 1;
    public override void Act()
    {
        DealDamage();
    }
}
