using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class AdrenalineShot : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 0);


        public override string Name => "Adrenaline Shot";
        public override string Description => "Your next card is played twice.";

        public override void OnPlayed(params object[] args)
        {
            Combat.Player.StatusEffectTarget.Apply<Adrenaline>(1);
        }
    }
}