using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageFadeIn : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    [SerializeField] private float delayToFadeIn;
    [SerializeField] private float durationToFadeIn;
    [SerializeField] private float startingAlpha;
    private float originalAlpha;

    private void Start()
    {
        originalAlpha = imageToFade.color.a;
    }

    private void OnEnable()
    {
        imageToFade.DOFade(startingAlpha, 0);
        StartCoroutine(CoroutineDoFade());        
    }

    IEnumerator CoroutineDoFade()
    {
        yield return new WaitForSeconds(delayToFadeIn);
        imageToFade.DOFade(originalAlpha, durationToFadeIn);
    }
}
