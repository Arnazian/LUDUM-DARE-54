public abstract class AbstractEnemy
{
    public abstract CappedInt ActCooldown { get; set; }
    public abstract CappedInt Health { get; set; }    
    public abstract void Act();
}
