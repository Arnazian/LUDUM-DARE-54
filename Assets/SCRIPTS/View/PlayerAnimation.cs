using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    void Start()
    {
        Combat.OnEventLogChanged += OnEvent;
    }

    private void OnEvent(CombatEvent e)
    {
        if (e.Target != GameSession.Player) return;
        e.Accept();
        switch (e.Type)
        {
            case CombatEvent.EventType.TurnStarted:
                StartCoroutine(TurnStartedAnimation(e));
                break;
            case CombatEvent.EventType.TurnEnded:
                StartCoroutine(TurnEndedAnimation(e));
                break;
            case CombatEvent.EventType.StatusApplied:
                StartCoroutine(StatusAppliedAnimation(e));
                break;
            case CombatEvent.EventType.StatusRemoved:
                StartCoroutine(StatusRemovedAnimation(e));
                break;
            case CombatEvent.EventType.Damaged:
                StartCoroutine(BeenDamagedAnimation(e));
                break;
            case CombatEvent.EventType.Healed:
                StartCoroutine(BeenHealedAnimation(e));
                break;
            case CombatEvent.EventType.Killed:
                StartCoroutine(KilledAnimation(e));
                break;
            case CombatEvent.EventType.TakenAction:
                StartCoroutine(TakenActionAnimation(e));
                break;
        }

    }

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnEvent;
    }

    IEnumerator TurnStartedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator TurnEndedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }



    IEnumerator StatusAppliedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator StatusRemovedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator BeenDamagedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator BeenHealedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator KilledAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator TakenActionAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

}
