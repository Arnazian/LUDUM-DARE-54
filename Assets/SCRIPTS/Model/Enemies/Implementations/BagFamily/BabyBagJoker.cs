using UnityEngine;

public class BabyBagJoker : AbstractEnemy
{
    public override CappedInt ActCooldown { get; set; } = new(3, 3);
    protected override CappedInt Health { get; set; } = new(10, 10);
    public override string PrefabName => "BabyBag";

    public BabyBagJoker()
    {
        EffectTarget.Apply<Animative>(-1);
    }

    public override void Act()
    {
        DealDamage(2);
        foreach (var enemy in Combat.Active.Enemies)
        {
            if (enemy == this) continue;
            enemy.ActCooldown.Value--;
            Combat.Active.PushCombatEvent(CombatEvent.CooldownChanged(enemy, enemy.ActCooldown.Value));
        }
    }

    public class Animative : AbstractStatusEffect
    {
        public override Color Color => new Color(1f, .8f, .8f);
        public override string Name => "Animative";
        public override string Description => "Animates all allies to act sooner.";
    }
}
