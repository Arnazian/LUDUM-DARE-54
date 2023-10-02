using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IStatusEffectTarget
{
    protected Dictionary<Type, AppliedEffect> EffectStacks { get; } //negative values should be treated as permanent
    public IReadOnlyList<AbstractStatusEffect> StatusEffects => EffectStacks.Values.Select(s => s.effect).ToList();

    public enum ApplyBlending
    {
        Add, Max
    }

    public static void OnBeginTurn(IStatusEffectTarget target) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnBeginTurn(); }
    public static void OnEndTurn(IStatusEffectTarget target) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnEndTurn(); }
    public static void OnAfterTargetSelection(IStatusEffectTarget target, ref object[] TargetSelections) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnAfterTargetSelection(ref TargetSelections); }
    public static void OnBeforeDoDamage(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnBeforeDealDamage(ref amount); }
    public static void OnBeforeRecieveDamage(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnBeforeRecieveDamage(ref amount); }
    public static void OnBeforeRecieveHealing(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnBeforeRecieveHeal(ref amount); }
    public static void OnBeforeDoHealing(IStatusEffectTarget target, ref int amount) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnBeforeDoHealing(ref amount); }
    public static void OnAfterAction(IStatusEffectTarget target, Action Action) { foreach (var effect in target.EffectStacks.ToList()) effect.Value.effect.OnAfterAction(Action); }


    public int GetStacks<EffectType>() => GetStacks(typeof(EffectType));
    public int GetStacks(Type type) => EffectStacks.TryGetValue(type, out var effect) ? effect.stacks : 0;

    public void Cleanse()
    {
        var effects = EffectStacks.Values.ToArray();
        foreach (var effect in effects)
            effect.effect.OnRemove();
        EffectStacks.Clear();
    }

    public void Apply<EffectType>(int stacks, ApplyBlending blending = ApplyBlending.Add) where EffectType : AbstractStatusEffect, new() => Apply(typeof(EffectType), stacks, blending);
    public void Apply(Type type, int stacks, ApplyBlending blending = ApplyBlending.Add)
    {
        if (stacks == 0) return; //do nothing                
        if (!EffectStacks.TryGetValue(type, out var appliedEffect))
        {
            var effect = (AbstractStatusEffect)Activator.CreateInstance(type);
            effect.EffectTarget = this;
            EffectStacks.Add(type, new(effect, stacks));
            effect.OnApply();
            appliedEffect = new(effect, stacks);
        }
        else
        {
            stacks = blending == ApplyBlending.Max ? Mathf.Max(stacks, GetStacks(type)) : stacks + GetStacks(type);
            EffectStacks[type] = new(EffectStacks[type].effect, stacks);
        }
        Combat.Active.PushCombatEvent(CombatEvent.ApplyStatus(this, appliedEffect.effect, appliedEffect.stacks));
    }

    public void Remove<EffectType>(int stacks = -1) where EffectType : AbstractStatusEffect => Remove(typeof(EffectType), stacks);
    public void Remove(Type type, int stacks = -1)
    {
        if (stacks == 0) return; //do nothing
        if (!EffectStacks.ContainsKey(type)) return; //effect not found        

        var reducedStatusEffect = EffectStacks[type];
        reducedStatusEffect.stacks -= stacks < 0 ? reducedStatusEffect.stacks : stacks; //if stacks negative, remove all
        if (reducedStatusEffect.stacks == 0)
        {
            var effect = EffectStacks[type].effect;
            EffectStacks.Remove(type); //remove if counter at exactly 0. negative effects stay
            effect.OnRemove();
        }
        else EffectStacks[type] = reducedStatusEffect;
        Combat.Active.PushCombatEvent(CombatEvent.RemoveStatus(this, reducedStatusEffect.effect, reducedStatusEffect.stacks));
    }

    protected struct AppliedEffect
    {
        public AbstractStatusEffect effect;
        public int stacks;

        public AppliedEffect(AbstractStatusEffect effect, int stacks)
        {
            this.effect = effect;
            this.stacks = stacks;
        }
    }
}
