using UnityEngine;

[RequireComponent(typeof(Canvas)), ExecuteAlways]
public class CanvasPlaneDistanceDriver : MonoBehaviour
{
    Canvas cached_Canvas;
    Canvas Canvas => cached_Canvas == null ? cached_Canvas = GetComponent<Canvas>() : cached_Canvas;

    public void Update()
    {
        if (float.IsNaN(Canvas.planeDistance)) 
            Canvas.planeDistance = 1f;
        var currentScale = Canvas.transform.lossyScale.x;
        if (float.IsNaN(Canvas.transform.lossyScale.x)) 
            currentScale = 1f;
        Canvas.planeDistance /= currentScale;
    }    
}
