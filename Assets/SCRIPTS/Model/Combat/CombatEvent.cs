public class CombatEvent
{
    public enum EventType
    {
        TurnStarted,
        TurnEnded,
        CooldownChanged,
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
        if (users <= 0) Combat.Active?.ConsumeCombatEvent(this);
    }
    public void Accept() => users++;
    public static CombatEvent Killed(object target) => new(EventType.Killed, target);
    public static CombatEvent Damaged(object target, int currentHealth, int amount) => new(EventType.Damaged, target, currentHealth, amount);
    public static CombatEvent Healed(object target, int currentHealth, int amount) => new(EventType.Healed, target, currentHealth, amount);
    public static CombatEvent TurnStarted(object target) => new(EventType.TurnStarted, target);
    public static CombatEvent TurnEnded(object target) => new(EventType.TurnEnded, target);

    public static CombatEvent TakenAction(object target) => new(EventType.TakenAction, target);
    public static CombatEvent CooldownChanged(object target, int cooldown, object context = null) => new(EventType.CooldownChanged, target, cooldown, context);

    public static CombatEvent ApplyStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusApplied, target, status, stacks);
    public static CombatEvent RemoveStatus(object target, AbstractStatusEffect status, int stacks) => new(EventType.StatusRemoved, target, status, stacks);
}