using UnityEngine;

public class Poisoned : AbstractStatusEffect
{
    public override Color Color => new Color(.5f, 1f, .2f);
    public override string Name => "Poisoned";
    public override string Description => "Deals 1 damage per stack.";

    public override void OnEndTurn()
    {
        DamageableTarget?.RecieveDamage(Stacks);
    }
}
