using System;

public interface IStatusEffect
{
    public virtual void OnApply(IStatusEffectTarget target) { }
    public virtual void OnRemove(IStatusEffectTarget target) { }
    public virtual void OnBeginTurn(IStatusEffectTarget target) { }
    public virtual void OnEndTurn(IStatusEffectTarget target) { }
    public virtual void OnBeforeDealDamage(IStatusEffectTarget target, ref int amount) { }
    public virtual void OnBeforeRecieveDamage(IStatusEffectTarget target, ref int amount) { }
    public virtual void OnBeforeRecieveHeal(IStatusEffectTarget target, ref int amount) { }
    public virtual void OnBeforeDoHealing(IStatusEffectTarget target, ref int amount) { }
    public virtual void OnAfterTargetSelection(IStatusEffectTarget target, ref object[] TargetSelections) { } //used to either randomize targets or add additional targets to the list
    public virtual void OnAfterAction(IStatusEffectTarget target, Action Action) { } //used for repeating attacks/actions
}
