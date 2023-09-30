using System.Collections.Generic;

public class Player
{
    public CappedInt Health { get; private set; }
    public CappedInt Block { get; private set; }

    public const int CardCapacity = 5;
    public List<AbstractCard> cards { get; private set; } = new(CardCapacity);
}