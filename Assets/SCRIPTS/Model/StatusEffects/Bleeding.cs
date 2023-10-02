using UnityEngine;

public class Bleeding : AbstractStatusEffect
{
    public override Color Color => new Color(.5f, .0f, .2f);
    public override string Name => "Bleeding";
    public override string Description => "Deals 1 damage per 2 stacks.<br>Increases each round.<br>Cured by any healing.";

    public override void OnEndTurn()
    {
        DamageableTarget?.RecieveDamage((Stacks + 1) / 2);
        Add(1); //count **UP** until recieving any heal
    }

    public override void OnBeforeRecieveHeal(ref int amount)
    {
        this.RemoveFully();
    }
}
