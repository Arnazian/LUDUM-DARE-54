using UnityEngine;

[RequireComponent(typeof(Canvas)), ExecuteAlways]
public class CanvasPlaneDistanceDriver : MonoBehaviour
{
    Canvas cached_Canvas;
    Canvas Canvas => cached_Canvas == null ? cached_Canvas = GetComponent<Canvas>() : cached_Canvas;

    public void Update()
    {
        var currentScale = Canvas.transform.lossyScale.x;
        Canvas.planeDistance /= currentScale;
    }
}
