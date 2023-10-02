using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ControlSelectionRotator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectionRotatorArt;
    [SerializeField] private float durationToFadeInSeconds;
    private float originalAlpha;

    private void Awake()
    {
        originalAlpha = selectionRotatorArt.color.a;
    }


    public void EnableRotatorVisuals()
    {
        selectionRotatorArt.DOFade(originalAlpha, durationToFadeInSeconds);
    }

    public void DisableRotatorVisuals()
    {
        selectionRotatorArt.DOFade(0, durationToFadeInSeconds);
    }
}
