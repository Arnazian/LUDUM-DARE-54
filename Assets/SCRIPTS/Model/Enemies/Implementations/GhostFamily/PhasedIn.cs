using UnityEngine;

public class PhasedIn : AbstractStatusEffect
{
    public override Color Color => new Color(.8f, .4f, 1f);
    public override string Name => "Visible";
    public override string Description => "Becomes invisible next turn";

    public override void OnBeginTurn()
    {
        RemoveFully();
        EffectTarget.Apply<PhasedOut>(-1);
    }
}