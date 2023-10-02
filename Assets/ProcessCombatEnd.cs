using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

public class ProcessCombatEnd : MonoBehaviour
{
    void Start()
    {
        Combat.OnEventLogChanged += OnCombatEvent;
    }

    void OnCombatEvent(CombatEvent e)
    {
        if (e.Type != CombatEvent.EventType.Killed) return;
        if (e.Target == GameSession.Player)
            StartCoroutine(SwitchStateRoutine(GameSession.State.GAME_OVER));
        else if (Combat.Active.Enemies.Count == 0)
        {
            GameSession.OfferedCard = CardGroups.GetRandom(Combat.Active.difficulty);
            if (GameSession.Player.Cards.Any(c => c?.GetType() == GameSession.OfferedCard.GetType()))
                GameSession.OfferedCard = CardGroups.GetRandom(Combat.Active.difficulty); //reroll once if type is already in hand
            GameSession.ActiveCombat = null;
            StartCoroutine(SwitchStateRoutine(GameSession.State.LOOT));
        }
    }
    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnCombatEvent;
    }

    IEnumerator SwitchStateRoutine(GameSession.State state)
    {
        yield return new WaitForSeconds(1f);
        GameSession.GameState = state;
    }
}
