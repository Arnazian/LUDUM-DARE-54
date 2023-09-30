using System;
using System.Collections.Generic;

public class Player
{
    public CappedInt Health { get; private set; } = new(20, 20);
    public CappedInt Block { get; private set; } = new(0, int.MaxValue);

    public const int CardCapacity = 5;
    public List<AbstractCard> Cards { get; private set; } = new(CardCapacity) {
        new Cards.Dagger(),
        new Cards.Dagger(),
        new Cards.Shield(),
        new Cards.Shield(),
        new Cards.Potion(),
    };

    public event Action<AbstractCard> OnCardRemoved;
    public event Action<AbstractCard> OnCardAdded;
    public event Action<AbstractCard, AbstractCard> OnReplaceCard;

    public void RemoveCard(AbstractCard card)
    {
        Cards.Remove(card);
        OnCardRemoved?.Invoke(card);
    }

    public void AddCard(AbstractCard card)
    {
        Cards.Add(card);
        OnCardAdded?.Invoke(card);
    }

    public void ReplaceCard(AbstractCard old, AbstractCard replacement)
    {
        var index = Cards.IndexOf(old);
        Cards[index] = replacement;
        OnReplaceCard?.Invoke(old, replacement);
    }

    public void OnStartTurn()
    {
        //status effects and stuff
    }

    public void OnCombatEnd()
    {
        foreach (var card in Cards)
        {
            card.Cooldown.Minimize();
            Block.Minimize();
        }
    }
}