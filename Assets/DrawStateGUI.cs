using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStateGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(15, 15, 100, 50), GameSession.GameState.ToString());
    }
}
