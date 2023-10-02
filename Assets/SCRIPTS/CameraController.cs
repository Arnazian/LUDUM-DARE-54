using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector2Spring PositionSpring;
    [SerializeField] private Spring.Config springConfig;
    private bool canMove = true;
    private void Start()
    {
        PositionSpring = new(springConfig);
        PositionSpring.OnSpringUpdated += (pos) => transform.position = new(pos.x, pos.y, transform.position.z);
    }

    private void Update()
    {
        HandleCameraMovement();
    }

    void HandleCameraMovement()
    {
        if (!canMove) return;
        PositionSpring.RestingPos = target.position;
        PositionSpring.Step(Time.deltaTime);
    }

    public void ScreenShake(float duration, float magnitude)
    {
        canMove = false;        
        transform.DOShakePosition(duration, magnitude).OnComplete(() =>
        {
            canMove = true;
        });
        
    }
}
