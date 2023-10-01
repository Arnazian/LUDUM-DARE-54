using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawStateGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(15, 15, 500, 30), GameSession.GameState.ToString());
        if (GameSession.GameState == GameSession.State.COMBAT)
        {
            var lastEvent = GameSession.ActiveCombat.ImmutableEventLogHistory.Last();
            GUI.Label(new Rect(15, 45, 500, 30), $"{lastEvent.Type}({lastEvent.Target})");
        }
    }
}
