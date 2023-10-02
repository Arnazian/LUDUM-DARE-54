using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class ActionCoolDownVisuals : MonoBehaviour
{
    [Header("Tweening Variables")]
    [SerializeField] private Vector2 newScale;
    [SerializeField] private float scalingDuration;
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;

    [SerializeField] ParticleSystem finishedParticles;


    [SerializeField] private Transform actionCoolDownObject;
    [SerializeField] private TextMeshProUGUI coolDownText;
    private Vector2 originalPosition;
    private Vector2 originalScale;

    private void Awake()
    {
        originalPosition = actionCoolDownObject.localPosition;
        originalScale = actionCoolDownObject.transform.localScale;
    }
    public void AnimatedCoolDownChange()
    {
        actionCoolDownObject.DOScale(newScale, scalingDuration).OnComplete(() =>
        {
            finishedParticles?.Play();
            // actionCoolDownObject.DOScale(originalScale, scalingDuration);
            
            actionCoolDownObject.DOShakePosition(shakeDuration, shakeStrength).OnComplete(() =>
            {
                actionCoolDownObject.DOLocalMove(originalPosition, 0.2f);
                actionCoolDownObject.DOScale(originalScale, scalingDuration);
                
                // change cd number
            });
            
        });
    }
}
