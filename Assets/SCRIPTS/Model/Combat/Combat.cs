using System;
using System.Collections.Generic;
using System.Linq;

public class Combat
{
    public EncounterGroups.Difficulty difficulty;
    public static Combat Active => GameSession.ActiveCombat;
    public static Player Player => GameSession.Player;
    public List<AbstractEnemy> Enemies { get; private set; }
    public int TurnCount { get; private set; }

    private readonly List<CombatEvent> EventLogHistory = new();
    public IReadOnlyList<CombatEvent> ImmutableEventLogHistory => EventLogHistory;
    private readonly Queue<CombatEvent> EventLog = new();
    public void ConsumeCombatEvent(CombatEvent e)
    {
        if (EventLog.Count == 0) return; //queue empty
        if (EventLog.Peek() == e) EventLog.Dequeue();
        EventLogHistory.Add(e);
        if (EventLog.Count == 0) return; //queue empty
        e = EventLog.Peek();
        _OnEventLogChanged?.Invoke(e);
        if (e.users == 0 && EventLog.Count > 0) ConsumeCombatEvent(e);
    }

    public void PushCombatEvent(CombatEvent e)
    {
        EventLog.Enqueue(e);
        if (EventLog.Count > 1) return; //'e' not visible yet
        _OnEventLogChanged?.Invoke(EventLog.Peek());
        if (e.users == 0) ConsumeCombatEvent(e);
    }

    private static event Action<CombatEvent> _OnEventLogChanged;
    public static event Action<CombatEvent> OnEventLogChanged
    {
        add
        {
            _OnEventLogChanged += value;
            if (Active?.EventLog.Count > 0)
                value?.Invoke(Active.EventLog.Peek());
        }
        remove => _OnEventLogChanged -= value;
    }

    public Combat(List<AbstractEnemy> enemies, EncounterGroups.Difficulty difficulty)
    {
        Enemies = enemies;
        this.difficulty = difficulty;
    }

    public void Pass()
    {
        foreach (var c in Player.Cards.Where(c => c != null))
            c.Cooldown.Value--;
        EnemyTurn();
    }
    private void EnemyTurn()
    {
        PushCombatEvent(CombatEvent.TurnEnded(Player));
        IStatusEffectTarget.OnEndTurn(Player);
        ProcessEnemies();
        if (GameSession.GameState != GameSession.State.COMBAT) return; // combat over, exit early
        IStatusEffectTarget.OnBeginTurn(Player);
        PushCombatEvent(CombatEvent.TurnStarted(Player));
        if(Player.Cards.Where(card => card != null).All(card => card.Cooldown.Value > 0)) Pass(); //Autoskip
    }

    public void PlayCard(AbstractCard card, params object[] args)
    {
        IStatusEffectTarget.OnAfterTargetSelection(Player, ref args);
        card.OnPlayed(args);
        IStatusEffectTarget.OnAfterAction(Player, () => card.OnPlayed(args));
        foreach (var c in Player.Cards.Where(c => c != null))
            c.Cooldown.Value--;
        card.Cooldown.Maximize();
        PushCombatEvent(CombatEvent.TakenAction(Player));

        //End Turn
        EnemyTurn();
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
        }
    }
}
