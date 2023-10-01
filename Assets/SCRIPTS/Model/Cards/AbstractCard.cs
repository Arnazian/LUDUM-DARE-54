using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCard
{
    public abstract CappedInt Cooldown { get; set; }
    public abstract void OnPlayed(params object[] args);

    public abstract string Name { get; }
    public abstract string Description { get; }

    public virtual Type[] Selections => new Type[0];
}
