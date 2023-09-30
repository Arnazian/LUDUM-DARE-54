using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTravel : MonoBehaviour
{
    [SerializeField] private float distanceToTravel;
    [SerializeField] private float moveDurationInSeconds;
    [SerializeField] private GameObject playerSpriteObject;

    void Start()
    {
        GameSession.OnStateChanged += OnStateChanged;
    }

    void OnDestroy() => GameSession.OnStateChanged -= OnStateChanged;

    void OnStateChanged(GameSession.State state)
    {
        if(state == GameSession.State.POST_COMBAT)
            MovePlayer();
    }

    public void MovePlayer()
    {
        float targetPositionX = playerSpriteObject.transform.position.x + distanceToTravel;
        playerSpriteObject.transform.DOMoveX(targetPositionX, moveDurationInSeconds).OnComplete(() =>
        {
            GameSession.GameState = GameSession.State.LOOT;
        });
    }
}

