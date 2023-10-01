using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : AbstractStatusEffect
{
    public override Color Color => new Color(1f, .5f, .0f);
    public override string Name => "Burning";
    public override string Description => "Deals 1 damage per 2 stacks.";

    public override void OnEndTurn() {
        DamageableTarget?.RecieveDamage((Stacks + 1) / 2);
        Remove(1); //count down;    
    }
}
