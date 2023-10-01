using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSelectionVisualizer : MonoBehaviour
{
    private Vector2Spring PositionSpring;
    [SerializeField] float DotsPerUnit = .5f;
    [SerializeField] RectTransform Crosshair;
    [SerializeField] RectTransform DotTemplate;
    private List<RectTransform> Dots { get; } = new();
    [SerializeField] AnimationCurve lineCurvature = AnimationCurve.Constant(0, 1, 0);

    Canvas cached_RootCanvas;
    Canvas RootCanvas => cached_RootCanvas ??= this.GetRootComponent<Canvas>();

    Vector2 StartPosition;
    Vector2? OverridePosition;

    [SerializeField] Spring.Config config = new(20, .6f);
    void Start()
    {
        PositionSpring = new(config);
        ICardTarget.OnBeginSelection += Init;
        ICardTarget.OnFinishSelection += End;

        ICardTarget.OnEnter += OnEnter;
        ICardTarget.OnExit += OnExit;

        PositionSpring.OnSpringUpdated += pos =>
        {
            Crosshair.position = (Vector3)pos + Crosshair.position.z * Vector3.forward;
            HandleDots(StartPosition, pos);
        };
    }

    void Update()
    {
        if (!Crosshair.gameObject.activeSelf) return;
        var pos = OverridePosition ?? RootCanvas.worldCamera.ScreenToWorldPoint(new(Input.mousePosition.x, Input.mousePosition.y, RootCanvas.planeDistance));
        PositionSpring.RestingPos = pos;
        PositionSpring.Step(Time.deltaTime);
    }

    void HandleDots(Vector2 from, Vector2 to)
    {
        var len = (from - to).magnitude;
        var dotCount = Mathf.CeilToInt(len * DotsPerUnit);
        while (Dots.Count > dotCount)
        {
            Destroy(Dots[0].gameObject);
            Dots.RemoveAt(0);
        }
        while (Dots.Count < dotCount)
        {
            var dot = Instantiate(DotTemplate, transform);
            Dots.Add(dot);
        }
        var delta = (to - from) / dotCount;
        for (int i = 0; i < dotCount; i++)
        {
            var pos = delta * (i + .5f) + from;
            var cross = Vector2.Perpendicular(delta);
            if (cross.y < 0) cross = -cross;
            pos += cross * lineCurvature.Evaluate(i / (float)dotCount) * len;
            Dots[i].position = new(pos.x, pos.y, Dots[i].position.z);
        }
    }

    void Init(Vector2 startPosition)
    {
        this.StartPosition = startPosition;
        PositionSpring.Position = StartPosition;
        Crosshair.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    void End(object _)
    {
        Crosshair.gameObject.SetActive(false);
        while (Dots.Count > 0)
        {
            Destroy(Dots[0].gameObject);
            Dots.RemoveAt(0);
        }
        Cursor.visible = true;
        OverridePosition = null;
    }

    void OnEnter(Vector2 p) => OverridePosition = RootCanvas.worldCamera.ScreenToWorldPoint(new(p.x, p.y, RootCanvas.planeDistance));
    void OnExit() => OverridePosition = null;

}
