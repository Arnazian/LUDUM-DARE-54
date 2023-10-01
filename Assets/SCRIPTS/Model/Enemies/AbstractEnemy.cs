using System;
using System.Collections.Generic;

public abstract class AbstractEnemy : IStatusEffectTarget, IDamageable
{
    protected abstract CappedInt ActCooldown { get; set; }
    protected abstract CappedInt Health { get; set; }
    public IReadOnlyCappedInt ReadOnlyActCooldown => ActCooldown;
    public IReadOnlyCappedInt ReadOnlyHealth => Health;
    Dictionary<Type, IStatusEffectTarget.AppliedEffect> IStatusEffectTarget.EffectStacks { get; } = new();
    IReadOnlyCappedInt IDamageable.Health => this.Health;

    public abstract void Act();
    public void DoTurn()
    {
        IStatusEffectTarget.OnBeginTurn(this);
        if (ActCooldown.Value > 0)
        {
            ActCooldown.Value--;
            return;
        }       
        Combat.Active.PushCombatEvent(CombatEvent.TakenAction(this));
        Act();
        IStatusEffectTarget.OnAfterAction(this, Act);
        ActCooldown.Maximize();
        IStatusEffectTarget.OnEndTurn(this);
    }

    public void DealDamage(int amount) => DealDamage(amount, GameSession.Player);
    public void DealDamage(int amount, params IDamageable[] targets)
    {
        IStatusEffectTarget.OnBeforeDoDamage(this, ref amount);
        foreach (var target in targets)
            target.RecieveDamage(amount);
    }

    public void DoHealing(int amount) => DoHealing(amount, this);
    public void DoHealing(int amount, params IDamageable[] targets)
    {
        IStatusEffectTarget.OnBeforeDoHealing(this, ref amount);
        foreach (var target in targets)
            target.RecieveHealing(amount);
    }

    public void Die()
    {
        Health.Value = 0;
        GameSession.ActiveCombat.Enemies.Remove(this);
        GameSession.ActiveCombat.PushCombatEvent(CombatEvent.Killed(this));
    }

    public void RecieveDamage(int amount)
    {
        IStatusEffectTarget.OnBeforeRecieveDamage(this, ref amount);
        Health.Value -= amount;
        GameSession.ActiveCombat.PushCombatEvent(CombatEvent.Damaged(this, Health.Value));
        if (Health.Value <= 0) Die();
    }

    public void RecieveHealing(int amount)
    {
        IStatusEffectTarget.OnBeforeRecieveHealing(this, ref amount);
        Health.Value += amount;
        GameSession.ActiveCombat.PushCombatEvent(CombatEvent.Healed(this, Health.Value));
        if (Health.Value <= 0) Die();
    }
}
