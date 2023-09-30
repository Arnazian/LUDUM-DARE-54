
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Dagger : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override void OnPlayed(List<AbstractEnemy> targets)
        {
            Debug.Log("Dagger played");
        }
    }
}