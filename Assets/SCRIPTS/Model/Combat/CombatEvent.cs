public class CombatEvent
{
    public enum EventType
    {
        TurnStarted,
        TurnEnded,
        Damaged,
        Healed,
        Killed,
        StatusApplied,
        StatusRemoved,
    }
    public EventType Type { get; }
    public object Target { get; }
    public object[] Args { get; }

    public CombatEvent(EventType type, object target, params object[] args)
    {
        Type = type;
        Target = target;
        Args = args;
    }
    public void Consume() => GameSession.ActiveCombat?.ConsumeCombatEvent(this);
    public static CombatEvent Killed(object target) => new(EventType.Killed, target);
    public static CombatEvent Damaged(object target, int amount) => new(EventType.Damaged, target, amount);
    public static CombatEvent Healed(object target, int amount) => new(EventType.Healed, target, amount);
    public static CombatEvent TurnStarted(object target) => new(EventType.TurnStarted, target);
    public static CombatEvent TurnEnded(object target) => new(EventType.TurnEnded, target);

    public static CombatEvent ApplyStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusApplied, target, status, stacks);
    public static CombatEvent RemoveStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusRemoved, target, status, stacks);
}