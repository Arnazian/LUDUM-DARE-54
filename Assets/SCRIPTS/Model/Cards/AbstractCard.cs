using System.Collections.Generic;

public abstract class AbstractCard
{
    public abstract CappedInt Cooldown { get; set; }
    public abstract void OnPlayed(List<AbstractEnemy> targets);
}
