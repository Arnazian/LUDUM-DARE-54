
using System;
using UnityEngine;


namespace Cards
{
    public class Timebomb : AbstractCard
    {
        public override CappedInt Cooldown { get; set; } = new(0, 5);

        public override string Name => "Time Bomb";
        public override string Description => "After 3 turns, deal 3 damage to all enemies.";

        public override void OnPlayed(params object[] args)
        {
            foreach (var enemy in Combat.Active.Enemies) enemy.EffectTarget.Apply<Timebombed>(3);
        }
    }
}