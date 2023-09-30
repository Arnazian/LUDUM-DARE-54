using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerTravel : MonoBehaviour
{
    [SerializeField] private float distanceToTravel;
    [SerializeField] private float moveDurationInSeconds;
    [SerializeField] private GameObject playerSpriteObject;

    public void MovePlayer()
    {
        float targetPositionX = playerSpriteObject.transform.position.x + distanceToTravel;
        playerSpriteObject.transform.DOMoveX(targetPositionX, moveDurationInSeconds).OnComplete(() =>
        {
            GameStateController.instance.ExitTravelling();
        });
    }
}

