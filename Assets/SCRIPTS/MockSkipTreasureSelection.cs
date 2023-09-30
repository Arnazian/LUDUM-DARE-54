using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockSkipTreasureSelection : MonoBehaviour
{    
    void LateUpdate()
    {
        if (GameSession.GameState == GameSession.State.LOOT)
        {
            GameSession.GameState = GameSession.State.PRE_COMBAT;
        }
    }
}
