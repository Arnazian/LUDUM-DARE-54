using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;



public class GrowIntoPlace : MonoBehaviour
{
    [SerializeField] private float timeToGrowInSeconds;
    [SerializeField] private float delayToStartGrowing;
    [SerializeField] private Vector3 sizeToStartFrom;
    [SerializeField] private GameObject hudToEnable;
    private Vector3 originalSize;

    private void OnEnable()
    {
        hudToEnable.SetActive(false);
        originalSize = transform.localScale;
        StartCoroutine(CoroutineDoGrowIn());
    }

    IEnumerator CoroutineDoGrowIn()
    {
        transform.DOScale(sizeToStartFrom, 0f);
        yield return new WaitForSeconds(delayToStartGrowing);
        transform.DOScale(originalSize, timeToGrowInSeconds).OnComplete(() =>
        {
            hudToEnable.SetActive(true);
        });
    }
}



