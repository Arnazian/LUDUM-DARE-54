using System;
using System.Collections.Generic;
using System.Linq;

public class Combat
{
    public static Combat Active => GameSession.ActiveCombat;
    public static Player Player => GameSession.Player;
    public List<AbstractEnemy> Enemies { get; private set; }
    public int TurnCount { get; private set; }

    private readonly Queue<CombatEvent> EventLog = new();
    public void ConsumeCombatEvent(CombatEvent e)
    {
        if (EventLog.Peek() == e) EventLog.Dequeue();
        if (EventLog.Count == 0) return; //queue empty
        OnEventLogChanged.Invoke(EventLog.Peek());
    }
    public void PushCombatEvent(CombatEvent e)
    {
        EventLog.Enqueue(e);
        if (EventLog.Count > 1) return; //'e' not visible yet
        OnEventLogChanged.Invoke(EventLog.Peek());
    }
    public event Action<CombatEvent> OnEventLogChanged;

    public Combat(List<AbstractEnemy> enemies)
    {
        Enemies = enemies;
    }

    public void Pass()
    {
        ProcessEnemies();
    }

    public void PlayCard(AbstractCard card, params object[] args)
    {                
        card.OnPlayed(args);

        foreach (var c in Player.Cards.Where(c => c != null))
            c.Cooldown.Value--;
        card.Cooldown.Maximize();

        //End Turn
        ProcessEnemies();
    }

    public void ProcessEnemies()
    {
        PushCombatEvent(CombatEvent.EnemyTurnStarted);
        foreach (var enemy in Enemies.ToList())
            enemy.DoTurn();
        if (Enemies.Count == 0) //won combat
        {
            Player.OnCombatEnd();
            GameSession.ActiveCombat = null;
            GameSession.GameState = GameSession.State.LOOT;
        }
        else Player.OnStartTurn();
    }
}
