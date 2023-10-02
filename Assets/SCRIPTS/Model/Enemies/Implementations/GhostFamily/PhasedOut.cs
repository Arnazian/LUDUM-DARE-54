using System;
using UnityEngine;

public class PhasedOut : AbstractStatusEffect
{
    public override Color Color => new Color(.2f, 0f, .4f);
    public override string Name => "Phased-Out";
    public override string Description => "Cannot Take Damage. Phases-In to attack.";

    public override void OnBeforeRecieveDamage(ref int amount)
    {
        amount = 0;
    }

    public override void OnAfterAction(Action a)
    {
        RemoveFully();
        EffectTarget.Apply<PhasedIn>(1);
    }
}