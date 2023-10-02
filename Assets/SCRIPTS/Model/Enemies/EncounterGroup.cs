using System.Collections.Generic;

public static class EncounterGroups
{
    public static Difficulty[] DiffByEncounterCounter = new Difficulty[] {
        Difficulty.Easy,
        Difficulty.Easy,
        Difficulty.Medium,
        Difficulty.Medium,
        Difficulty.Hard,
        Difficulty.Boss,
    };

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
        new() { new Pumpkin()},
        new() { new BabyBag()},
        new() { new Ghosty()},
        new() { new Ghosty(), new Ghosty()},        
    };

    public static List<List<AbstractEnemy>> Medium => new()
    {
        
        new() { new BabyBag(), new Ghosty()},
        new() { new Pumpkin(), new Pumpkin(), new Pumpkin()},
        new() { new Pumpkin(), new Pumpkin(), new Ghosty()},
        new() { new Ghosty(), new Ghosty(), new Ghosty(), new Ghosty() },        
        new() { new BabyBag(), new BabyBag()},
    };

    public static List<List<AbstractEnemy>> Hard => new()
    {
         new() { new WitchSkull()},
         new() { new WitchSkull(), new Ghosty()},
         new() { new BabyBag(), new BabyBag(), new BabyBag()},
         new() { new BabyBag(), new BabyBag(), new Pumpkin(), new Pumpkin()},
         new() { new BabyBag(), new BabyBag(), new Ghosty(), new Ghosty()},
    };

    public static List<List<AbstractEnemy>> Boss => new()
    {
        new() { new WitchSkull(), new Ghosty(), new Ghosty(), new Ghosty(), new Ghosty()}
    };
}