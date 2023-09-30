public class CombatEvent
{
    public enum EventType
    {
        EnemyTurnStarted,
        PlayerTurnStarted,
        Damaged,
        Healed,
        Killed,
        StatusApplied,
    }
    public EventType Type { get; }
    public object[] Args { get; }

    public CombatEvent(EventType type, params object[] args)
    {
        Type = type;
        Args = args;
    }
    public void Consume() => GameSession.ActiveCombat.ConsumeCombatEvent(this);
    public static CombatEvent Killed(AbstractEnemy enemy) => new(EventType.Killed, enemy);
    public static CombatEvent Damaged(AbstractEnemy enemy, int amount) => new(EventType.Damaged, enemy, amount);
    public static CombatEvent EnemyTurnStarted => new(EventType.EnemyTurnStarted);
}