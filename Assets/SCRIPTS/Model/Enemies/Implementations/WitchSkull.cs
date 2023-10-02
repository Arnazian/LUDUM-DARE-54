using System;
using UnityEngine;

public class WitchSkull : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(10, 10);
    protected override CappedInt Health { get; set; } = new(20, 20);
    public override string PrefabName => "WitchSkull";

    public WitchSkull()
    {
        // EffectTarget.Apply<HexGenerator>(-1);
    }

    public override void Act()
    {
        DealDamage(15);
    }

    public class HexGenerator : AbstractStatusEffect
    {
        public override Color Color => new Color(.8f, .1f, .8f);
        public override string Name => "Witches' Legacy";
        public override string Description => "Gains a random Hex at the end of its turn.";

        public override void OnEndTurn()
        {
            switch (UnityEngine.Random.Range(0, 5))
            {
                case 0:
                    EffectTarget.Apply<BlurHex>(1, IStatusEffectTarget.ApplyBlending.Add);
                    break;
                case 1:
                case 2:
                    EffectTarget.Apply<PowerHex>(1, IStatusEffectTarget.ApplyBlending.Add);
                    break;
                case 3:
                case 4:
                    EffectTarget.Apply<ArmorHex>(1, IStatusEffectTarget.ApplyBlending.Add);
                    break;
            }
        }
    }

    public class BlurHex : AbstractStatusEffect
    {
        public override Color Color => new Color(1f, 1f, .6f);
        public override string Name => "Hex: Blur";
        public override string Description => "Takes an additional action.";

        public override void OnAfterAction(Action Action)
        {
            for (int i = 0; i < Stacks; i++)
                Action?.Invoke();
            RemoveFully();
        }
    }

    public class PowerHex : AbstractStatusEffect
    {
        public override Color Color => new Color(1f, .6f, 1f);
        public override string Name => "Hex: Power";
        public override string Description => "Next action deals 1 additional damage per stack.";

        public override void OnBeforeDealDamage(ref int amount)
        {
            amount += Stacks;
            RemoveFully();
        }
    }

    public class ArmorHex : AbstractStatusEffect
    {
        public override Color Color => new Color(.2f, .15f, .5f);
        public override string Name => "Hex: Block";
        public override string Description => "Absorbs 1 damage per stack.";

        public override void OnBeforeRecieveDamage(ref int amount)
        {
            var min = Mathf.Min(amount, Stacks);
            amount -= Stacks;
            Remove(min);
        }
    }
}

