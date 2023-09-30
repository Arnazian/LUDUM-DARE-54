public abstract class AbstractEnemy
{
    protected abstract CappedInt ActCooldown { get; set; }
    protected abstract CappedInt Health { get; set; }
    public IReadOnlyCappedInt ReadOnlyActCooldown => ActCooldown;
    public IReadOnlyCappedInt ReadOnlyHealth => Health;

    public abstract void Act();
    public void DoTurn()
    {
        ActCooldown.Value--;
        if (ActCooldown.Value > 0) return;
        Act();
        ActCooldown.Maximize();
    }
    public void Damage(int amount)
    {
        Health.Value -= amount;
        GameSession.ActiveCombat.PushCombatEvent(CombatEvent.Damaged(this, amount));
        if (Health.Value <= 0) Kill();
    }

    public void Kill()
    {
        Health.Value = 0;
        GameSession.ActiveCombat.Enemies.Remove(this);
        GameSession.ActiveCombat.PushCombatEvent(CombatEvent.Killed(this));
    }
}
