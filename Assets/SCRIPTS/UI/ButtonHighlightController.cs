using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlightController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Object References")]
    [SerializeField] private Transform transformToModify;
    [SerializeField] private GameObject particlesToEnable;

    [Header("Hovered Variables")]
    [SerializeField] private Vector3 scaleWhenHovered;
    [SerializeField] private Vector3 positionOffsetWhenHovered;
    [SerializeField] private float durationToScale;
    [SerializeField] private float durationToMove;

    [SerializeField] private bool constantAnimate = false;

    //private ButtonLineTweening buttonLineTweening;
    private List<Tween> tweens = new List<Tween>();
    private Vector3 originalScale;
    private Vector3 originalPosition;



    private void Awake()
    {
        if (transformToModify == null)
            transformToModify = transform;

        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }
    private void Start()
    {
        // particlesToEnable?.SetActive(false);
        //buttonLineTweening = GetComponent<ButtonLineTweening>();
    }

    private void Update()
    {
        DoConstantAnimation();
    }

    void DoConstantAnimation()
    {
        if (!constantAnimate) return;
        
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        HighlightObject();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UnHighlightObject();
    }

    void HighlightObject()
    {
        KillAllTweens();
        Vector3 positionWithOffset = new Vector3(originalPosition.x + positionOffsetWhenHovered.x, 
            originalPosition.y + positionOffsetWhenHovered.y, originalPosition.z + positionOffsetWhenHovered.z);

        Tween moveTween = transformToModify.DOLocalMove(positionWithOffset, durationToMove);
        Tween scaleTween = transformToModify.DOScale(scaleWhenHovered, durationToScale);
        tweens.Add(moveTween);
        tweens.Add(scaleTween);
        //buttonLineTweening.Expand();
        // particlesToEnable?.SetActive(true);
    }

    void UnHighlightObject()
    {
        KillAllTweens();
        Tween moveTween = transformToModify.DOLocalMove(originalPosition, durationToMove);
        Tween scaleTween = transformToModify.DOScale(originalScale, durationToScale);
        tweens.Add(moveTween);
        tweens.Add(scaleTween);
       // particlesToEnable?.SetActive(false);
    }

    void KillAllTweens()
    {
        foreach (Tween tween in tweens)
        {
            if (tween.IsActive())
            {
                tween.Kill();
            }
        }
        tweens.Clear();
    }

}
