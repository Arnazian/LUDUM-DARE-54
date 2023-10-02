using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MischievousPumpkin : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(5, 5);
    protected override CappedInt Health { get; set; } = new(4, 4);
    public override string PrefabName => "Pumpkin";
    public override int Damage => 2;
    public MischievousPumpkin()
    {
        this.EffectTarget.Apply<Mischievous>(-1);
    }

    public override void Act()
    {
        DealDamage();
    }

    public class Mischievous : AbstractStatusEffect
    {
        public override Color Color => new Color(.3f, .3f, .3f);
        public override string Name => "Mischievous";
        public override string Description => "After attacking, puts a random player card on cooldown.";

        public override void OnAfterAction(Action Action)
        {
            var cardNotOnCD = Combat.Player.Cards.Where(card => (card?.Cooldown?.Normalized ?? 1) < 1f).ToArray();
            if (cardNotOnCD.Length == 0) return;
            var randomCard = cardNotOnCD[UnityEngine.Random.Range(0, cardNotOnCD.Length)];
            randomCard.Cooldown.Maximize();
            Combat.Active.PushCombatEvent(CombatEvent.CooldownChanged(Combat.Player, randomCard.Cooldown.Value, randomCard));
        }
    }
}
