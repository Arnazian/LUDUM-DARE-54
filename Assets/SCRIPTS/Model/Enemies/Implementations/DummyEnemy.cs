using UnityEngine;

public class DummyEnemy : AbstractEnemy
{
    protected override CappedInt ActCooldown { get; set; } = new(2, 2);
    protected override CappedInt Health { get; set; } = new(10, 10);

    public override void Act()
    {
        GameSession.Player.DoDamage(5);
    }
}