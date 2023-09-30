using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RoundData[] easyRounds;
    [SerializeField] private RoundData[] normalRounds;
    [SerializeField] private RoundData[] hardRounds;
    [SerializeField] private RoundData[] bossRounds;

    enum difficultyPerRound {Easy, Normal, Hard, BOSS }
    [SerializeField] difficultyPerRound[] roundDifficulties;

    List<GameObject> enemiesToSpawn = new List<GameObject>();
    Combat activeCombat = Combat.ActiveCombat;

    public void SpawnEnemies()
    {
        foreach(GameObject go in enemiesToSpawn)
        {
            activeCombat.Enemies.Add(new DummyEnemy());
        }
    }

    /*
    void ChooseEnemiesToSpawn()
    {
        int curRound = GameStateController.instance.curRound - 1;
        difficultyPerRound curDifficulty = roundDifficulties[curRound];

        switch (curDifficulty)
        {
            case difficultyPerRound.Easy:
                enemiesToSpawn = easyRounds[Random.Range(0, easyRounds.Length)];
                break;
            case difficultyPerRound.Normal:
                selectedRound = normalRounds;
                break;
            case difficultyPerRound.Hard:
                selectedRound = hardRounds;
                break;
            case difficultyPerRound.Boss:
                selectedRound = bossRounds;
                break;
            default:
                Debug.LogError("Invalid round difficulty");
                break;
        }
    }
    */

}
