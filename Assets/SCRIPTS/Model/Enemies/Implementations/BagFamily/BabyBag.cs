using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBag : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(3, 3);
    protected override CappedInt Health { get; set; } = new(5, 5);
    public override string PrefabName => "BabyBag";

    public BabyBag()
    {
        ((IStatusEffectTarget)this).Apply<Shy>(-1);
    }

    public override void Act()
    {
        DealDamage(5);
    }

    public class Shy : AbstractStatusEffect
    {
        public override Color Color => new Color(.8f, .8f, .8f);
        public override string Name => "Shy";
        public override string Description => "Resets action timer upon taking damage.";

        public override void OnBeforeRecieveDamage(ref int amount)
        {
            if (EffectTarget is AbstractEnemy)
            {
                (EffectTarget as AbstractEnemy).ActCooldown?.Maximize();
                Combat.Active.PushCombatEvent(CombatEvent.CooldownChanged(EffectTarget, (EffectTarget as AbstractEnemy).ActCooldown.Value));
            }
        }
    }
}
