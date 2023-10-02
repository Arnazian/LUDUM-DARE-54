using UnityEngine;

public class SpectralAid : AbstractStatusEffect
{
    public override Color Color => new Color(.8f, .6f, 1f);
    public override string Name => "Spectral Aid";
    public override string Description => "Your next attack deals 5 extra damage.";

    public override void OnBeforeDealDamage(ref int amount)
    {
        amount += 5;
        RemoveFully();
    }
}
