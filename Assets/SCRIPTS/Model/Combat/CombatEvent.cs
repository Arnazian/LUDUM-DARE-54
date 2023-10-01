public class CombatEvent
{
    public enum EventType
    {
        TurnStarted,
        TurnEnded,
        Damaged,
        Healed,
        Killed,
        TakenAction,
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
    public int users = 0;
    public void Consume()
    {
        users--;
        if (users <= 0) GameSession.ActiveCombat?.ConsumeCombatEvent(this);
    }
    public void Accept() => users++;
    public static CombatEvent Killed(object target) => new(EventType.Killed, target);
    public static CombatEvent Damaged(object target, int amount) => new(EventType.Damaged, target, amount);
    public static CombatEvent Healed(object target, int amount) => new(EventType.Healed, target, amount);
    public static CombatEvent TurnStarted(object target) => new(EventType.TurnStarted, target);
    public static CombatEvent TurnEnded(object target) => new(EventType.TurnEnded, target);

    public static CombatEvent TakenAction(object target) => new(EventType.TakenAction, target);

    public static CombatEvent ApplyStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusApplied, target, status, stacks);
    public static CombatEvent RemoveStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusRemoved, target, status, stacks);
}