using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints;


    [SerializeField] private EnemyComponent EnemyTemplate;
    private readonly List<EnemyComponent> spawnedEnemies = new();

    void Start()
    {
        GameSession.OnStateChanged += OnStateChanged;
        OnStateChanged(GameSession.GameState);
    }

    private void OnStateChanged(GameSession.State state)
    {
        switch (state)
        {
            case GameSession.State.COMBAT:
                SpawnEnemies();
                break;
            case GameSession.State.LOOT:
                break;
        }
    }

    // instantiate visuals at start of combat
    public void SpawnEnemies()
    {
        int enemyCount = GameSession.ActiveCombat.Enemies.Count;
        for (int i = 0; i < enemyCount; i++)
        {
            var EnemyInstance = Instantiate(EnemyTemplate, spawnPoints[i]);
            EnemyInstance.enemy = GameSession.ActiveCombat.Enemies[i];
            spawnedEnemies.Add(EnemyInstance);
        }
    }
}
