using System;
using System.Collections.Generic;
using System.Linq;

public class Combat
{
    public static Combat Active => GameSession.ActiveCombat;
    public static Player Player => GameSession.Player;
    public List<AbstractEnemy> Enemies { get; private set; }
    public int TurnCount { get; private set; }

    private readonly List<CombatEvent> EventLogHistory = new();
    public IReadOnlyList<CombatEvent> ImmutableEventLogHistory => EventLogHistory;
    private readonly Queue<CombatEvent> EventLog = new();
    public void ConsumeCombatEvent(CombatEvent e)
    {
        if (EventLog.Peek() == e) EventLog.Dequeue();
        EventLogHistory.Add(e);
        if (EventLog.Count == 0) return; //queue empty
        _OnEventLogChanged?.Invoke(EventLog.Peek());
    }
    public void PushCombatEvent(CombatEvent e)
    {
        EventLog.Enqueue(e);
        if (EventLog.Count > 1) return; //'e' not visible yet
        _OnEventLogChanged?.Invoke(EventLog.Peek());
    }

    private static event Action<CombatEvent> _OnEventLogChanged;
    public static event Action<CombatEvent> OnEventLogChanged
    {
        add
        {
            _OnEventLogChanged += value;
            if (GameSession.ActiveCombat?.EventLog.Count > 0)
                value?.Invoke(GameSession.ActiveCombat.EventLog.Peek());
        }
        remove => _OnEventLogChanged -= value;
    }

    public Combat(List<AbstractEnemy> enemies)
    {
        Enemies = enemies;
    }

    public void Pass()
    {
        PushCombatEvent(CombatEvent.TurnEnded(Player));
        IStatusEffectTarget.OnEndTurn(Player);
        ProcessEnemies();
        if (GameSession.GameState != GameSession.State.COMBAT) return; // combat over, exit early
        IStatusEffectTarget.OnBeginTurn(Player);
        PushCombatEvent(CombatEvent.TurnStarted(Player));
    }

    public void PlayCard(AbstractCard card, params object[] args)
    {
        IStatusEffectTarget.OnAfterTargetSelection(Player, ref args);
        card.OnPlayed(args);
        IStatusEffectTarget.OnAfterAction(Player, () => card.OnPlayed(args));
        foreach (var c in Player.Cards.Where(c => c != null))
            c.Cooldown.Value--;
        card.Cooldown.Maximize();

        //End Turn
        Pass();
    }

    public void ProcessEnemies()
    {
        foreach (var enemy in Enemies.ToList())
        {
            PushCombatEvent(CombatEvent.TurnStarted(enemy));
            enemy.DoTurn();            
            PushCombatEvent(CombatEvent.TurnEnded(enemy));
        }
        if (Enemies.Count == 0) //won combat
        {
            Player.OnCombatEnd();
            GameSession.ActiveCombat = null;
            GameSession.GameState = GameSession.State.LOOT;
        }
    }
}
