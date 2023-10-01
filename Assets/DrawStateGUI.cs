using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawStateGUI : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Label(new Rect(15, 15, 500, 30), GameSession.GameState.ToString());
    }
}
