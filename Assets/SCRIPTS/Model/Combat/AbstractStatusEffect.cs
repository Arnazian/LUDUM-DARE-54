using System;
using UnityEngine;

public abstract class AbstractStatusEffect
{

    #region Display Attributes
    public abstract Color Color { get; }
    public abstract string Name { get; }
    public abstract string Description { get; }
    #endregion

    public IStatusEffectTarget EffectTarget { get; set; }
    public IDamageable DamageableTarget => EffectTarget as IDamageable;

    public int Stacks => EffectTarget.GetStacks(GetType());
    public void Add(int amount) => EffectTarget.Apply(GetType(), amount);
    public void Remove(int amount) => EffectTarget.Remove(GetType(), amount);
    public void RemoveFully() => EffectTarget.Remove(GetType());

    public virtual void OnApply() { }
    public virtual void OnRemove() { }
    public virtual void OnBeginTurn() { }
    public virtual void OnEndTurn() { }
    public virtual void OnBeforeDealDamage(ref int amount) { }
    public virtual void OnBeforeRecieveDamage(ref int amount) { }
    public virtual void OnBeforeRecieveHeal(ref int amount) { }
    public virtual void OnBeforeDoHealing(ref int amount) { }
    public virtual void OnAfterTargetSelection(ref object[] TargetSelections) { } //used to either randomize targets or add additional targets to the list
    public virtual void OnAfterAction(Action Action) { } //used for repeating attacks/actions
}
