using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoShieldVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shield;
    [SerializeField] private ParticleSystem shieldParticles;
    [SerializeField] private float shieldUpDuration;
    [SerializeField] private float shieldFadeInDuration;
    [SerializeField] private float shieldFadeOutDuration;

    [SerializeField] private AudioSource blockAudioSource;
    void Start()
    {
        shield.DOFade(0, 0);
    }
    public void StartShieldVisuals()
    {
        StartCoroutine(CoroutineStartShieldVisuals());
    }

    IEnumerator CoroutineStartShieldVisuals()
    {
        blockAudioSource.Play();
        shield.DOFade(1, shieldFadeInDuration);
        shieldParticles.Play();
        yield return new WaitForSeconds(shieldUpDuration);
        shield.DOFade(0, shieldFadeOutDuration);
    }
}
