using UnityEngine;

public class DummyEnemy : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(0, 0);
    public override CappedInt Health { get; set; } = new(10, 10);

    public override void Act()
    {
        Debug.Log("Dummy: ...");
    }
}