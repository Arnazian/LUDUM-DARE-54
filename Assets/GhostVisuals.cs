using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GhostVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ghostSprite;
    [SerializeField] private float transAlpha;
    [SerializeField] private float fadeDurationInSeconds;
    private float originalAlpha;
    private void Start()
    {
        originalAlpha = ghostSprite.color.a;
        ghostSprite.DOFade(transAlpha, fadeDurationInSeconds);
    }
    public void DoFadeIn()
    {
        ghostSprite.DOFade(originalAlpha, fadeDurationInSeconds);
    }
    public void DoFadeOut()
    {
        ghostSprite.DOFade(transAlpha, fadeDurationInSeconds);
    }
    
}
