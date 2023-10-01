using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector2Spring PositionSpring;
    [SerializeField] private Spring.Config springConfig;
    private void Start()
    {
        PositionSpring = new(springConfig);
        PositionSpring.OnSpringUpdated += (pos) => transform.position = new(pos.x, pos.y, transform.position.z);
    }

    private void Update()
    {
        PositionSpring.RestingPos = target.position;
        PositionSpring.Step(Time.deltaTime);
    }
}
