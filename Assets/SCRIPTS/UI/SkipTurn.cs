using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkipTurn : MonoBehaviour
{
    public void DoSkipTurn() => Combat.Active?.Pass();

    void Start()
    {
        Combat.OnEventLogChanged += OnCombatEvent;
    }

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnCombatEvent;
    }

    void OnCombatEvent(CombatEvent e)
    {
        if (e.Type == CombatEvent.EventType.TurnStarted && e.Target == Combat.Player)
            if (Combat.Player.Cards.Where(card => card != null).All(card => card.Cooldown.Value > 0)) DoSkipTurn();
    }
}
