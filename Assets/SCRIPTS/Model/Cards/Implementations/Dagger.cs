
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class Dagger : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 3);
        private const int Damage = 5;

        public override void OnPlayed(List<AbstractEnemy> targets)
        {
            Debug.Log("Dagger played");
            foreach (var target in targets)
                target.Damage(Damage);
        }
    }
}