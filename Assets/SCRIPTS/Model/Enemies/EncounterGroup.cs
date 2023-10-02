using System.Collections.Generic;

public static class EncounterGroups
{
    public enum Difficulty
    {
        Easy, Medium, Hard, Boss
    }
    public static Dictionary<Difficulty, List<List<AbstractEnemy>>> EncountersByDifficulty => new()
    {
        {Difficulty.Easy, Easy},
        {Difficulty.Medium, Medium},
        {Difficulty.Hard, Hard},
        {Difficulty.Boss, Boss},
    };

    public static List<List<AbstractEnemy>> Easy => new()
    {
        new() { new BabyBag(), new Ghosty()},
        new() { new BabyBag(), new Pumpkin()},
        new() { new Pumpkin(), new Ghosty()},
        new() { new MischievousPumpkin()},

        new() { new Pumpkin(), new Pumpkin()},
        new() { new Ghosty(), new Ghosty()},
        new() { new BabyBag(), new BabyBag()},
    };

    public static List<List<AbstractEnemy>> Medium => new()
    {
        new() { new BabyBag(), new Ghosty(), new Pumpkin()},
        new() { new BabyBag(), new Ghosty(), new Pumpkin()},
        new() { new BabyBag(), new Ghosty(), new Pumpkin()},

        new() { new BabyBag(), new BabyBag(), new Ghosty()},
        new() { new BabyBag(), new BabyBag(), new Pumpkin()},
        new() { new Pumpkin(), new Pumpkin(), new BabyBag()},

        new() { new Pumpkin(), new Ghosty(), new Ghosty()},
        new() { new Pumpkin(), new Pumpkin(), new Pumpkin()},
        new() { new BabyBag(), new BabyBag(), new BabyBag()},

        new() { new Pumpkin(), new VengefulPumpkin()},
        new() { new Pumpkin(), new MischievousPumpkin()},

        new() { new BabyBag(), new BabyBagJoker(), new Ghosty()},
        new() { new BabyBag(), new BabyBagGrumpy(), new Ghosty()},
        new() { new BabyBag(), new BabyBagQueen(), new Ghosty()},

    };

    public static List<List<AbstractEnemy>> Hard => new()
    {
         new() { new BabyBag(), new BabyBagJoker(), new VengefulPumpkin(), new Pumpkin()},
         new() { new BabyBag(), new BabyBagGrumpy(), new Ghosty(), new Ghosty()},
         new() { new Pumpkin(), new Pumpkin(), new Ghosty(), new Ghosty()},


         new() { new Pumpkin(), new VengefulPumpkin(), new MischievousPumpkin()},
         new() { new BabyBag(), new BabyBagGrumpy(), new BabyBagJoker(), new BabyBagQueen()}

    };

    public static List<List<AbstractEnemy>> Boss => new()
    {
        new() { new WitchSkull(), new Pumpkin(), new Pumpkin(), new Ghosty(), new BabyBag()}
    };
}