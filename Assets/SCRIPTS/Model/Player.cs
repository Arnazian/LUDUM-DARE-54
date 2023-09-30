using System;
using System.Collections.Generic;
using System.Linq;

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

    public void OnStartTurn()
    {
        //status effects and stuff
    }

    public void OnCombatEnd()
    {
        foreach (var card in Cards.Where(card => card != null))
        {
            card.Cooldown.Minimize();
            Block.Minimize();
        }
    }
}