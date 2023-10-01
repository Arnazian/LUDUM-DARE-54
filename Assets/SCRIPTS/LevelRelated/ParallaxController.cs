using DG.Tweening;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] environmentSprites;
    [SerializeField] private float[] parallaxSpeeds;
    private Transform mainCamera;

    private Vector3[] initialPositions;

    private void Start()
    {
        mainCamera = Camera.main.transform;
        initialPositions = new Vector3[environmentSprites.Length];

        for (int i = 0; i < environmentSprites.Length; i++)
        {
            initialPositions[i] = environmentSprites[i].position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < environmentSprites.Length; i++)
        {
            float parallaxX = (mainCamera.position.x - transform.position.x) * -parallaxSpeeds[i];

            Vector3 newPosition = initialPositions[i] + new Vector3(parallaxX, 0, 0);
            environmentSprites[i].position = newPosition;
        }
    }
}
