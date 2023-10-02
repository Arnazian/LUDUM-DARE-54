using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class SpectralFavor : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override string Name => "Spectral Favor";
        public override string Description => "Your next attack deals 5 extra damage.";

        public override void OnPlayed(params object[] args)
        {            
            Combat.Player.StatusEffectTarget.Apply<SpectralAid>(1);
        }
    }
}