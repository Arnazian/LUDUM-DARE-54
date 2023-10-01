public interface IDamageable
{
    public IReadOnlyCappedInt Health { get; }
    public void RecieveDamage(int amount);
    public void RecieveHealing(int amount);
}
