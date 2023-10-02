using UnityEngine;

public class PhasedIn : AbstractStatusEffect
{
    public override Color Color => new Color(.8f, .4f, 1f);
    public override string Name => "Phased-In";
    public override string Description => "Phases-Out at the end of turn.";

    public override void OnBeginTurn()
    {
        RemoveFully();
        EffectTarget.Apply<PhasedOut>(-1);
    }
}