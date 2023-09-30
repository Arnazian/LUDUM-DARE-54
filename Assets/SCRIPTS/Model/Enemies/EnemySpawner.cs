using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;

    [Header("Difficulty Of Each Round")]
    [SerializeField] difficultyPerRound[] roundDifficulties;

    [Header("References To Rounds")]
    [SerializeField] private RoundData[] easyRounds;
    [SerializeField] private RoundData[] normalRounds;
    [SerializeField] private RoundData[] hardRounds;
    [SerializeField] private RoundData[] bossRounds;


    enum difficultyPerRound {Easy, Normal, Hard, BOSS }
    List<GameObject> enemiesToSpawn = new List<GameObject>();
    List<GameObject> curEnemiesAlive = new List<GameObject>();
    Combat activeCombat = Combat.ActiveCombat;

    public void SpawnEnemies()
    {
        ChooseEnemiesToSpawn();
        int enemyCount = enemiesToSpawn.Count;

        for (int i = 0; i < enemyCount; i++)
        {
            // activeCombat.Enemies.Add(new DummyEnemy());
            GameObject newEnemy = Instantiate(enemiesToSpawn[i], spawnPoints[i].position, Quaternion.identity);
            curEnemiesAlive.Add(newEnemy);
        }
    }

    
    void ChooseEnemiesToSpawn()
    {
        int curRound = GameStateController.instance.curRound - 1;
        difficultyPerRound curRoundDifficulty = roundDifficulties[curRound];

        switch (curRoundDifficulty)
        {
            case difficultyPerRound.Easy:
                enemiesToSpawn = easyRounds[Random.Range(0, easyRounds.Length)].enemiesToSpawn;
                break;
            case difficultyPerRound.Normal:
                enemiesToSpawn = normalRounds[Random.Range(0, normalRounds.Length)].enemiesToSpawn;
                break;
            case difficultyPerRound.Hard:
                enemiesToSpawn = hardRounds[Random.Range(0, hardRounds.Length)].enemiesToSpawn;
                break;
            case difficultyPerRound.BOSS:
                enemiesToSpawn = bossRounds[Random.Range(0, bossRounds.Length)].enemiesToSpawn;
                break;
            default:
                Debug.LogError("Invalid round difficulty");
                break;
        }
    }

    // temporary way to reach end of combat
    public void KillAllEnemies()
    {
        for (int i = curEnemiesAlive.Count - 1; i >= 0; i--)
        {
            GameObject enemyToTarget = curEnemiesAlive[i];
            curEnemiesAlive.RemoveAt(i);
            Destroy(enemyToTarget);

        }
        GameStateController.instance.ExitCombat();
    }
}
