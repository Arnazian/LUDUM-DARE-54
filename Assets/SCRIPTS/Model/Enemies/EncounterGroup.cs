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
        new() { new DummyEnemy()},
    };

    public static List<List<AbstractEnemy>> Medium => new()
    {
        new() { new DummyEnemy(), new DummyEnemy()}
    };

    public static List<List<AbstractEnemy>> Hard => new()
    {
        new() { new DummyEnemy(), new DummyEnemy(), new DummyEnemy()}
    };

    public static List<List<AbstractEnemy>> Boss => new()
    {
        new() { new DummyEnemy(), new DummyEnemy(), new DummyEnemy(), new DummyEnemy()}
    };
}