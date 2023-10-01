using System;
using System.Collections.Generic;
using UnityEngine;


public class StatusEffectList : MonoBehaviour
{
    [SerializeField] private StatusEffectComponent StatusEffectTemplate;
    private Dictionary<AbstractStatusEffect, StatusEffectComponent> Instances = new();
    private IStatusEffectTarget target;
    public IStatusEffectTarget Target
    {
        get => target;
        set
        {
            target = value;
            ClearAll();
            foreach (var status in target.StatusEffects)
                InstantiateEffectComponent(status);
        }
    }

    void Start()
    {
        Combat.OnEventLogChanged += OnCombatEvent;
    }

    private void OnCombatEvent(CombatEvent e)
    {
        if (e.Target != Target) return;

        switch (e.Type)
        {
            case CombatEvent.EventType.StatusApplied:
                var effect = e.Args[0] as AbstractStatusEffect;
                if (!Instances.ContainsKey(effect))
                    InstantiateEffectComponent(effect);
                break;
            case CombatEvent.EventType.StatusRemoved:
                effect = e.Args[0] as AbstractStatusEffect;
                if (Target.GetStacks(effect.GetType()) == 0)
                {
                    if (Instances[effect] != null) Destroy(Instances[effect].gameObject);
                    Instances[effect] = null;
                }
                break;
        }
    }

    private void ClearAll()
    {
        foreach (var effect in Instances)
            Destroy(effect.Value.gameObject);
        Instances.Clear();
    }

    private void InstantiateEffectComponent(AbstractStatusEffect effect)
    {
        var instance = Instantiate(StatusEffectTemplate, transform);
        instance.Target = Target;
        instance.Effect = effect;
        Instances[effect] = instance;
    }
}
