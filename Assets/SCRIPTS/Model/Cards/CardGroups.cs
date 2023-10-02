using System.Collections.Generic;
using System.Linq;

namespace Cards
{
    public static class CardGroups
    {
        public enum Rarity
        {
            Common, Rare, Legendary
        }

        public static AbstractCard GetRandom(EncounterGroups.Difficulty difficulty)
        {
            var weights = CardGroupWeightsByDifficulty[difficulty];
            var weightSum = weights.Sum(w => w.Value);
            var rand = new System.Random();
            var randomRoll = rand.NextDouble() * weightSum;
            var queue = new Queue<KeyValuePair<Rarity, float>>(weights);
            var rarity = Rarity.Common;
            while (randomRoll >= 0 && queue.Count > 0)
            {
                var pair = queue.Dequeue();
                randomRoll -= pair.Value;
                if (randomRoll >= 0) rarity = pair.Key;
            }
            var cards = CardsByRarity[rarity];
            return cards[rand.Next(cards.Length)];
        }


        public static Dictionary<EncounterGroups.Difficulty, Dictionary<Rarity, float>> CardGroupWeightsByDifficulty = new()
        {
            {
                EncounterGroups.Difficulty.Easy,
                new() {
                        {Rarity.Common,   .75f},
                        {Rarity.Rare,     .5f},
                        {Rarity.Legendary, .1f}
                }
            },
            {
                EncounterGroups.Difficulty.Medium,
                new() {
                        {Rarity.Common,   .5f},
                        {Rarity.Rare,     .75f},
                        {Rarity.Legendary, .1f}
                }
            },
            {
                EncounterGroups.Difficulty.Hard,
                new() {
                        {Rarity.Common,    .1f},
                        {Rarity.Rare,       1f},
                        {Rarity.Legendary, .5f}
                }
            },
            {
                EncounterGroups.Difficulty.Boss,
                new() {
                        {Rarity.Common,    0f},
                        {Rarity.Rare,      0f},
                        {Rarity.Legendary, 1f}
                }
            },
        };

        public static Dictionary<Rarity, AbstractCard[]> CardsByRarity => new()
        {
            {Rarity.Common, Common},
            {Rarity.Rare, Rare},
            {Rarity.Legendary, Legendary},

        };

        public static AbstractCard[] Common => new AbstractCard[] {
            new Dagger(),
            new Sword(),
            new Shield(),
            new Potion(),
        };

        public static AbstractCard[] Rare => new AbstractCard[] {
            new Greatsword(),
            new Boomerang(),
            new PoisonDagger(),
            new Timebomb(),
            new Haste(),
            new Mudbomb(),
            new AdrenalineShot(),
            new BloodRitual(),
        };

        public static AbstractCard[] Legendary => new AbstractCard[] {
            new Fireball(),
            new Deathmark(),
            new Greatsword(),
            new Meditation(),
            new SpectralFavor(),
        };



    }
}