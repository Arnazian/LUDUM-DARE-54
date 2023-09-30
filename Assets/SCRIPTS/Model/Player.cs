using System.Collections.Generic;

public class Player
{
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    public const int CardCapacity = 5;
    public List<AbstractCard> cards { get; private set; } = new(CardCapacity);
}