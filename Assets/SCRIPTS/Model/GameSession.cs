using System;
using System.Linq;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static Combat ActiveCombat { get; set; }
    public static Player Player { get; set; } = new();

    public enum State
    {
        COMBAT, POST_COMBAT, LOOT
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
            enemy.Damage(int.MaxValue);
        ActiveCombat.Pass();
    }

    private static int EncounterCounter;
    public void StartCombat()
    {
        ActiveCombat = new(EncounterGroups.Easy[EncounterCounter]);
        GameState = State.COMBAT;
    }
}
