using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MischievousPumpkin : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(5, 5);
    protected override CappedInt Health { get; set; } = new(8, 8);
    public override string PrefabName => "Pumpkin";

    public MischievousPumpkin()
    {
        this.EffectTarget.Apply<Vengeful>(-1);
    }

    public override void Act()
    {
        DealDamage(2);
    }

    public class Vengeful : AbstractStatusEffect
    {
        public override Color Color => new Color(.8f, .4f, .4f);
        public override string Name => "Mischievous";
        public override string Description => "After attacking, puts a random player card on cooldown.";

        public override void OnAfterAction(Action Action)
        {
            var cardNotOnCD = Combat.Player.Cards.Where(card => (card?.Cooldown?.Normalized ?? 1) < 1f).ToArray();
            if (cardNotOnCD.Length == 0) return;
            var randomCard = cardNotOnCD[UnityEngine.Random.Range(0, cardNotOnCD.Length)];
            randomCard.Cooldown.Maximize();
        }
    }
}
