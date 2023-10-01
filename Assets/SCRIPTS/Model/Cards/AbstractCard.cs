using System;
using System.Collections.Generic;

public abstract class AbstractCard
{
    public abstract CappedInt Cooldown { get; set; }
    public abstract void OnPlayed(params object[] args);
    public virtual Type[] Selections => new Type[0];
}
