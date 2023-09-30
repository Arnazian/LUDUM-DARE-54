using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public AbstractCard Card { get; set; }
    private Vector2Spring PositionSpring;
    private BaseSpring RotSpring;
    private DrivenSpring ScaleSpring;
    private DrivenSpring GlowEffectSpring;

    Canvas cached_RootCanvas;
    Canvas RootCanvas => cached_RootCanvas ??= this.GetRootComponent<Canvas>();

    CanvasGroup cached_RootCanvasGroup;
    CanvasGroup RootCanvasGroup => cached_RootCanvasGroup ??= this.GetRootComponent<CanvasGroup>();

    Canvas cached_DragVisualCanvas;
    Canvas DragVisualCanvas => cached_DragVisualCanvas ??= DragVisual?.GetComponent<Canvas>();

    CanvasGroup cached_CanvasGroup;
    CanvasGroup CanvasGroup => cached_CanvasGroup ??= DragVisual?.GetComponent<CanvasGroup>();

    RectTransform RectTransform => transform as RectTransform;


    [Header("Component References")]
    [SerializeField] Image Artwork;
    [SerializeField] Image GlowEffect;
    [SerializeField] Image CooldownFill;
    [SerializeField] TMP_Text CooldownText;
    [SerializeField] TMP_Text TitleText;

    [Header("Spring Settings")]
    [field: SerializeField] private Spring.Config springConfig = new(20f, .6f);

    [Header("Other Settings")]
    public float PlayYThreshold;
    private bool IsInPlayingArea => DragVisual.localPosition.y >= PlayYThreshold;
    private bool IsPlayable => Combat.Active != null && Card.Cooldown.Value <= 0;

    private State state;
    enum State
    {
        none = 0,
        hovered = 1,
        dragging = 2,
    }
    float Scale => state switch
    {
        State.hovered => 1.1f,
        State.dragging => 1.2f,
        State.dragging | State.hovered => 1.2f,
        _ => 1f
    };

    float GlowAlpha => state switch
    {
        State.hovered => .5f,
        State.dragging => IsInPlayingArea ? 1f : .5f,
        State.dragging | State.hovered => IsInPlayingArea ? 1f : .5f,
        _ => .3f
    };

    void Start()
    {
        PositionSpring = new(springConfig);
        RotSpring = new(springConfig);
        ScaleSpring = new(() => Scale, springConfig);
        GlowEffectSpring = new(() => GlowAlpha, springConfig);

        PositionSpring.OnSpringUpdated += pos =>
        {
            DragVisual.position = (Vector3)pos + new Vector3(0, 0, DragVisual.position.z);
            var rx = Mathf.Clamp(PositionSpring.Velocity.y * 3f, -50, 50);
            var ry = Mathf.Clamp(-PositionSpring.Velocity.x * 3f, -50, 50);
            DragVisual.rotation = Quaternion.Euler(rx, ry, DragVisual.eulerAngles.z);
        };
        RotSpring.OnSpringUpdated += r => DragVisual.rotation = Quaternion.Euler(DragVisual.eulerAngles.x, DragVisual.eulerAngles.y, r);
        ScaleSpring.OnSpringUpdated += s => DragVisual.localScale = new(s, s, s);
        GlowEffectSpring.OnSpringUpdated += alpha => GlowEffect.color = new(GlowEffect.color.r, GlowEffect.color.g, GlowEffect.color.b, alpha);
    }

    private RectTransform DragVisual => transform.GetChild(0) as RectTransform;
    public void OnBeginDrag(PointerEventData eventData)
    {
        state |= State.dragging;
        DragVisualCanvas.overrideSorting = true;
        DragVisualCanvas.sortingOrder = 1;
        RotSpring.RestingPos = 0f;

        CanvasGroup.blocksRaycasts = false;
        RootCanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        PositionSpring.RestingPos = RootCanvas.worldCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, RootCanvas.planeDistance));
        if (IsInPlayingArea && !IsPlayable)
        {
            eventData.pointerDrag = null;
            OnEndDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        state &= ~State.dragging;
        DragVisualCanvas.overrideSorting = false;
        DragVisualCanvas.sortingOrder = 1;
        CanvasGroup.blocksRaycasts = true;
        RootCanvasGroup.blocksRaycasts = true;

        if (IsInPlayingArea && IsPlayable) Combat.Active.PlayCard(Card);
    }

    //TODO: Idea: implement an interesting looking sorting algorithm that runs in a coroutine and slowly sorts the hand at the start of each turn by cost

    void Update()
    {
        if ((state & State.dragging) == 0)
        {
            PositionSpring.RestingPos = RectTransform.position + RectTransform.TransformDirection(RectTransform.sizeDelta * (new Vector2(.5f, .5f) - RectTransform.pivot));
            RotSpring.RestingPos = RectTransform.eulerAngles.z >= 180 ? RectTransform.eulerAngles.z - 360 : RectTransform.eulerAngles.z;
        }

        PositionSpring.Step(Time.deltaTime);
        RotSpring.Step(Time.deltaTime);
        ScaleSpring.Step(Time.deltaTime);
        GlowEffectSpring.Step(Time.deltaTime);

        CanvasGroup.interactable = Card.Cooldown.Value == 0;
        CooldownFill.fillAmount = Card.Cooldown.Normalized;
        CooldownText.text = Card.Cooldown.Value > 0 ? Card.Cooldown.Value.ToString() : "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        state |= State.hovered;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        state &= ~State.hovered;
    }
}
