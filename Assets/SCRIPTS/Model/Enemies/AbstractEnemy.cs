public abstract class AbstractEnemy
{
    public int Cooldown { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public abstract void Act();
}
