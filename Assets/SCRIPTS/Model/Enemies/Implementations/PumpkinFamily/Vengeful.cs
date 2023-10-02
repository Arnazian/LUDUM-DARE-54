using UnityEngine;

public class Vengeful : AbstractStatusEffect
{
    public override Color Color => new Color(.8f, .4f, .4f);
    public override string Name => "Vengeful";
    public override string Description => "Reduce action timer upon taking damage.";

    public override void OnBeforeRecieveDamage(ref int amount)
    {
        if (EffectTarget is AbstractEnemy) ((AbstractEnemy)EffectTarget).ActCooldown.Value -= 1;
    }
}