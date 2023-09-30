using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public int curRound = 0;
    private bool inCombat = false;
    private bool inTreasure = false;
    private bool isTravelling = false;

    private PlayerTravel playerTravel;
    private EnemySpawner enemySpawner;

    // private Combat activeCombat = Combat.ActiveCombat;

    private void Start()
    {
        enemySpawner = GetComponent<EnemySpawner>();
        playerTravel = GetComponent<PlayerTravel>();    
        EnterTravelling();
    }

    public void EnterCombat()
    {
        inCombat = true;
        curRound++;
        enemySpawner.SpawnEnemies();
        // spawn enemies
        // PlayerTurn();

    }

    public void ExitCombat()
    {
        inCombat = false;
        EnterTreasure();
    }

    public void EnterTreasure()
    {
        inTreasure = true;
        UIManager.instance.OpenTreasureView();
    }

    public void ExitTreasure()
    {
        inTreasure = false;
        UIManager.instance.CloseTreasureView();
        EnterTravelling();
    }

    public void EnterTravelling()
    {
        isTravelling = true;
        playerTravel.MovePlayer();
        // MoveToNextLocation();
    }

    public void ExitTravelling()
    {
        isTravelling = false;
        EnterCombat();
    }

    #region  Singleton implementation
    public static GameStateController instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    #endregion
}
