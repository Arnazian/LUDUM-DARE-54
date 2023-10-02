using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StatusEffectVisuals : MonoBehaviour
{
    [SerializeField] private Transform objectToScale;
    [SerializeField] private float timeToScale;
    [SerializeField] private float delayToScale;
    [SerializeField] private Vector2 newScale;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = objectToScale.localScale;
    }


    private void OnEnable()
    {
        StartCoroutine(CoroutineDoScale());
        objectToScale.DOScale(new Vector2(0f, 0f), 0);
    }

    IEnumerator CoroutineDoScale()
    {
        yield return new WaitForSeconds(delayToScale);
        objectToScale.DOScale(newScale, timeToScale).OnComplete(() =>
        {
            objectToScale.DOScale(originalScale, timeToScale);
        });
    }
}
