using UnityEngine;

public class TooltipFlip : MonoBehaviour
{
    RectTransform RectTransform => transform as RectTransform;
    Canvas cached_RootCanvas;
    public Canvas RootCanvas => cached_RootCanvas ??= this.GetRootComponent<Canvas>();

    public float threshold = .15f;

    [SerializeField] private float originalX;
    [SerializeField] private float flippedX;

    void OnEnable()
    {
        RectTransform.localPosition = new(originalX, RectTransform.localPosition.y, 0);
        var corners = new Vector3[4];
        RectTransform.GetWorldCorners(corners);
        var cam = RootCanvas.worldCamera ?? Camera.main;
        var rightEdge = cam.ViewportToWorldPoint(new(1, 0, RootCanvas?.planeDistance ?? 1f)).x;
        var flipped = rightEdge - corners[2].x < threshold;
        RectTransform.localPosition = new(flipped ? flippedX : originalX, RectTransform.localPosition.y, 0);
    }
}
