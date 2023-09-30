using UnityEngine;

[CreateAssetMenu(menuName = "LD54 /RoundData")]
public class RoundData : ScriptableObject
{
    [Header("Specify Spawn Point: 0 - 4")]
    public int[] spawnPointNumber;

    [Header("Prefab Information")]
    public GameObject[] prefabsToSpawn;
   
}
