using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private Transform target;
    private Vector3 velocity = Vector3.zero;

    private void FixedUpdate()
    {
        MoveToTarget();        
    }

    void MoveToTarget()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
