public abstract class AbstractEnemy
{
    public int Cooldown { get; set; }
    public CappedInt Health { get; set; }    
    public abstract void Act();
}
