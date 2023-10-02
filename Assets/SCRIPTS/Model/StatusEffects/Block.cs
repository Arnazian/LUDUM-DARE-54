using UnityEngine;

public class Block : AbstractStatusEffect
{
    public override Color Color => new Color(.15f, .2f, .5f);
    public override string Name => "Block";
    public override string Description => "Each stack mitigates 1 damage, then disappears";

    public override void OnBeforeRecieveDamage(ref int amount)
    {
        var min = Mathf.Min(Stacks, amount);
        amount -= min;
        Remove(min);
    }
}
