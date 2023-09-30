
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Shield : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);

        public override void OnPlayed(List<AbstractEnemy> targets)
        {
            Cooldown.Value = Cooldown.Max;
            Combat.Player.Block.Value += 5;
            Debug.Log("Shield played");
        }
    }
}