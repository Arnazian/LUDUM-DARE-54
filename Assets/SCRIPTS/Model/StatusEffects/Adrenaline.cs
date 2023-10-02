using System;
using UnityEngine;

public class Adrenaline : AbstractStatusEffect
{
    public override Color Color => new Color(1f, 1f, .6f);
    public override string Name => "Adrenaline";
    public override string Description => "Your next action triggers twice.";

    public override void OnAfterAction(Action action)
    {
        if (Stacks == 2)
            Remove(1);
        else
        {
            action.Invoke();
            RemoveFully();
        }
    }
}
