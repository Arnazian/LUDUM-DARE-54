using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipTurn : MonoBehaviour
{
    public void DoSkipTurn() => Combat.Active.Pass();
}
