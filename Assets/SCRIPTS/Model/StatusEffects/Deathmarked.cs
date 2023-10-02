using UnityEngine;

public class Deathmarked : AbstractStatusEffect
{
    public override Color Color => new Color(.2f, .0f, .1f);
    public override string Name => "Marked for death";
    public override string Description => "All Damage done to this is increased by 1.";

    public override void OnBeforeDealDamage(ref int amount)
    {
        amount += 1;
    }
}
