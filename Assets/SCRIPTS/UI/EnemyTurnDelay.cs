using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnDelay : MonoBehaviour
{
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
        if (e.Type == CombatEvent.EventType.TurnStarted && e.Target != Combat.Player)
            e.Accept();
        StartCoroutine(ConsumeDelayed(e));
    }

    IEnumerator ConsumeDelayed(CombatEvent e)
    {
        yield return new WaitForSeconds(.5f / (Combat.Active?.Enemies.Count ?? 1));
        e.Consume();
    }
}
