using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BabyBagQueen : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(4, 4);
    protected override CappedInt Health { get; set; } = new(10, 10);
    public override string PrefabName => "BabyBag";
    public override int Damage => 1;

    public BabyBagQueen()
    {
        EffectTarget.Apply<Royalty>(-1);
    }

    public override void Act()
    {
        var others = Combat.Active.Enemies.Where(e => e != this).ToArray();
        if (others.Length > 0)
        {
            var random = Random.Range(0, others.Length);
            var enemy = others[random];
            enemy.Act();
        }
        else DealDamage();
    }

    public class Royalty : AbstractStatusEffect
    {
        public override Color Color => new Color(1f, .8f, .8f);
        public override string Name => "Royalty";
        public override string Description => "Orders an ally to act in its place.";
    }
}
