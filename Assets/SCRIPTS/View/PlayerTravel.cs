using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerTravel : MonoBehaviour
{
    [SerializeField] private float distanceToTravel;
    [SerializeField] private float moveDurationInSeconds;
    [SerializeField] private GameObject playerSpriteObject;

    [SerializeField] private LevelLayoutController levelLayoutController;

    void Start()
    {
        GameSession.OnStateChanged += OnStateChanged;
    }

    void OnDestroy() => GameSession.OnStateChanged -= OnStateChanged;

    void OnStateChanged(GameSession.State state)
    {
        if (state == GameSession.State.POST_COMBAT)
            MovePlayer(() => GameSession.GameState = GameSession.State.LOOT);
        if (state == GameSession.State.PRE_COMBAT)
        {
            playerSpriteObject.transform.position = playerSpriteObject.transform.position - Vector3.right * distanceToTravel * 2;
            MovePlayer(GameSession.StartCombat);
        }
    }

    public void MovePlayer(Action actionAfter)
    {
        float targetPositionX = playerSpriteObject.transform.position.x + distanceToTravel;
        levelLayoutController.CreateNextLevelBlock();
        playerSpriteObject.transform.DOMoveX(targetPositionX, moveDurationInSeconds).OnComplete(() => actionAfter?.Invoke());
    }
}

