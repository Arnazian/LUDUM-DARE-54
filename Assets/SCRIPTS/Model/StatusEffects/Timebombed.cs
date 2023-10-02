using UnityEngine;

public class Timebombed : AbstractStatusEffect
{
    public override Color Color => new Color(.5f, .3f, .8f);
    public override string Name => "Timebombed";
    public override string Description => "Tick-Tock.";

    public override void OnBeginTurn()
    {
        if (Stacks == 1) DamageableTarget?.RecieveDamage(3);
        Remove(1); //it's the final count down. duduluduuu dudulududuuu
    }
}
