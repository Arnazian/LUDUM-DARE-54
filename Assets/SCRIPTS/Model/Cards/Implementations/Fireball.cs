
using System;
using System.Linq;
using UnityEngine;


namespace Cards
{
    public class Fireball : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);
        private const int Damage = 3;
        public override Type[] Selections => new[] { typeof(AbstractEnemy) };

        public override string Name => "Fireball";
        public override string Description => "Deal 2 damage. Apply 4 Burning to all enemies.";

        public override void OnPlayed(params object[] args)
        {
            GameSession.Player.DoDamage(Damage, args.Select(arg => arg as IDamageable).ToArray());            
            foreach(var enemy in Combat.Active.Enemies)
            {
                enemy.EffectTarget.Apply<Burning>(4);
            }
        }
    }
}