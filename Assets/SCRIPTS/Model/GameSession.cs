using System;
using System.Linq;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static Combat ActiveCombat { get; set; }
    public static AbstractCard OfferedCard { get; set; }
    public static Player Player { get; set; } = new();

    public enum State
    {
        COMBAT, LOOT, PRE_COMBAT, GAME_OVER
    }


    private static State gameState;
    public static State GameState
    {
        get => gameState;
        set
        {
            if (gameState == value) return;
            OnStateChanged?.Invoke(gameState = value);
        }
    }
    public static event Action<State> OnStateChanged;
    void Awake() => StartCombat();

    public void CheatKillAll()
    {
        foreach (var enemy in ActiveCombat.Enemies.ToList())
            enemy.Die();
        ActiveCombat.Pass();
    }


    private static readonly EncounterGroups.Difficulty[] Pattern = new EncounterGroups.Difficulty[]
    {
        EncounterGroups.Difficulty.Easy,
        EncounterGroups.Difficulty.Easy,
        EncounterGroups.Difficulty.Easy,
        EncounterGroups.Difficulty.Medium,
        EncounterGroups.Difficulty.Easy,
        EncounterGroups.Difficulty.Medium,
        EncounterGroups.Difficulty.Medium,
        EncounterGroups.Difficulty.Medium,
        EncounterGroups.Difficulty.Hard,
        EncounterGroups.Difficulty.Medium,
        EncounterGroups.Difficulty.Hard,
        EncounterGroups.Difficulty.Hard,
        EncounterGroups.Difficulty.Hard,
        EncounterGroups.Difficulty.Hard,
        EncounterGroups.Difficulty.Boss,
    };

    private static int EncounterCounter;
    public static void StartCombat()
    {
        var encounterOptions = EncounterGroups.EncountersByDifficulty[Pattern[EncounterCounter++]];
        var rand = new System.Random();
        ActiveCombat = new(encounterOptions[rand.Next(encounterOptions.Count)]);
        GameState = State.COMBAT;
        ActiveCombat.PushCombatEvent(CombatEvent.TurnStarted(Player));
    }
}
