using System;
using System.Collections.Generic;
using System.Linq;

public class Player : IStatusEffectTarget, IDamageable
{
    public CappedInt Health { get; private set; } = new(20, 20);
    IReadOnlyCappedInt IDamageable.Health => this.Health;
    public IReadOnlyCappedInt ReadOnlyHealth => this.Health;

    public IStatusEffectTarget StatusEffectTarget => this;

    public const int CardCapacity = 5;
    public List<AbstractCard> Cards { get; private set; } = new(CardCapacity) {
        new Cards.Dagger(),
        new Cards.Dagger(),
        new Cards.Shield(),
        new Cards.Shield(),
        new Cards.Potion(),
    };

    Dictionary<Type, IStatusEffectTarget.AppliedEffect> IStatusEffectTarget.EffectStacks { get; } = new();


    public event Action<int, AbstractCard> OnCardChanged;

    public void RemoveCard(AbstractCard card) => RemoveCardAt(Cards.IndexOf(card));
    public void RemoveCardAt(int slot)
    {
        Cards[slot] = null;
        OnCardChanged?.Invoke(slot, null);
    }

    public void AddCard(AbstractCard card)
    {
        Cards.Add(card);
        OnCardChanged?.Invoke(Cards.Count - 1, card);
    }

    public void ReplaceCard(AbstractCard old, AbstractCard replacement) => ReplaceCardAt(Cards.IndexOf(old), replacement);
    public void ReplaceCardAt(int index, AbstractCard replacement)
    {
        Cards[index] = replacement;
        OnCardChanged?.Invoke(index, replacement);
    }

    public void OnCombatEnd()
    {
        StatusEffectTarget.Cleanse();
        foreach (var card in Cards.Where(card => card != null))
            card.Cooldown.Minimize();
    }

    public void DoDamage(int amount, params IDamageable[] targets)
    {
        IStatusEffectTarget.OnBeforeDoDamage(this, ref amount);
        foreach (var target in targets)
        {
            target.RecieveDamage(amount);
        }
    }
    public void DoHealing(int amount, params IDamageable[] targets)
    {
        IStatusEffectTarget.OnBeforeDoHealing(this, ref amount);
        foreach (var target in targets)
        {
            target.RecieveHealing(amount);
        }
    }

    public void RecieveDamage(int amount)
    {
        IStatusEffectTarget.OnBeforeRecieveDamage(this, ref amount);
        Health.Value -= amount;
        Combat.Active.PushCombatEvent(CombatEvent.Damaged(this, Health.Value));
        if (Health.Value <= 0) Die();
    }

    public void RecieveHealing(int amount)
    {
        IStatusEffectTarget.OnBeforeRecieveHealing(this, ref amount);
        Health.Value += amount;
        Combat.Active.PushCombatEvent(CombatEvent.Healed(this, Health.Value));
        if (Health.Value <= 0) Die();
    }

    void Die()
    {
        GameSession.GameState = GameSession.State.GAME_OVER;
        //TODO: handle death
    }
}