using System;
using System.Collections.Generic;

public interface IStatusEffectTarget
{
    protected Dictionary<Type, AppliedEffect> EffectStacks { get; } //negative values should be treated as permanent

    public enum ApplyBlending
    {
        Add, Max
    }

    public static void OnBeginTurn(IStatusEffectTarget target) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnBeginTurn(target); }
    public static void OnEndTurn(IStatusEffectTarget target) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnEndTurn(target); }
    public static void OnAfterTargetSelection(IStatusEffectTarget target, ref object[] TargetSelections) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnAfterTargetSelection(target, ref TargetSelections); }
    public static void OnBeforeDoDamage(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnBeforeDealDamage(target, ref amount); }
    public static void OnBeforeRecieveDamage(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnBeforeRecieveDamage(target, ref amount); }
    public static void OnBeforeRecieveHealing(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnBeforeRecieveHeal(target, ref amount); }
    public static void OnBeforeDoHealing(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnBeforeDoHealing(target, ref amount); }
    public static void OnAfterAction(IStatusEffectTarget target, Action Action) { foreach (var effect in target.EffectStacks) effect.Value.effect.OnAfterAction(target, Action); }



    public virtual void Apply<EffectType>(int stacks, ApplyBlending blending = ApplyBlending.Max) where EffectType : IStatusEffect, new()
    {
        if (stacks == 0) return; //do nothing        
        if (!EffectStacks.ContainsKey(typeof(EffectType)))
        {
            var effect = Activator.CreateInstance<EffectType>();
            EffectStacks.Add(typeof(EffectType), new(effect, stacks));
            effect.OnApply(this);
        }
        var appliedEffect = EffectStacks[typeof(EffectType)];
        Combat.Active.PushCombatEvent(CombatEvent.ApplyStatus(this, appliedEffect.effect, appliedEffect.stacks));
    }

    public virtual void Remove<EffectType>(int stacks = -1) where EffectType : IStatusEffect
    {
        if (stacks == 0) return; //do nothing
        if (!EffectStacks.ContainsKey(typeof(EffectType))) return; //effect not found        

        var reducedStatusEffect = EffectStacks[typeof(EffectType)];
        reducedStatusEffect.stacks -= stacks < 0 ? reducedStatusEffect.stacks : stacks; //if stacks negative, remove all
        if (reducedStatusEffect.stacks == 0)
        {
            var effect = EffectStacks[typeof(EffectType)].effect;
            EffectStacks.Remove(typeof(EffectType)); //remove if counter at exactly 0. negative effects stay        
            effect.OnRemove(this);
        }
        else EffectStacks[typeof(EffectType)] = reducedStatusEffect;
        Combat.Active.PushCombatEvent(CombatEvent.RemoveStatus(this, reducedStatusEffect.effect, reducedStatusEffect.stacks));

    }

    protected struct AppliedEffect
    {
        public IStatusEffect effect;
        public int stacks;

        public AppliedEffect(IStatusEffect effect, int stacks)
        {
            this.effect = effect;
            this.stacks = stacks;
        }
    }
}
