using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGetHitEffects : MonoBehaviour
{
    [Header("Sprite References")]
    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private SpriteRenderer hitFlashSprite;

    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip getHitClip;

    [Header("Particle References")]
    [SerializeField] private ParticleSystem getHitParticles;

    [Header("Flash Values")]
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private float secondsToFadeSprite = 0.15f;

    [Header("Screen Shake Values")]
    [SerializeField] private float hitShakeDuration;
    [SerializeField] private float hitShakeStrength;
    [SerializeField] private float deathShakeDuration;
    [SerializeField] private float deathShakeStrength;

    private CameraController cameraController;
    private Sequence fadeMainToNormal;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        hitFlashSprite.DOFade(0, 0);
    }

    public void DoGetHitEffects(int damageAmount)
    {
        if (damageAmount <= 0) return;
        audioSource.clip = getHitClip;
        audioSource.Play();

        StartCoroutine(CoroutineHitFlash(damageAmount));       
    }

    IEnumerator CoroutineHitFlash(int damageAmount)
    {
        getHitParticles.Play();
        GetComponent<FloatingDamageNumbers>().StartMoveUp(damageAmount.ToString());
        cameraController.ScreenShake(hitShakeDuration, hitShakeStrength);
        hitFlashSprite.DOFade(1, secondsToFadeSprite);
        mainSprite.DOFade(0, secondsToFadeSprite);
        yield return new WaitForSeconds(flashDuration);
        hitFlashSprite.DOFade(0, secondsToFadeSprite);
        mainSprite.DOFade(1, secondsToFadeSprite);
    }

    public void DoDeathEffects()
    {
        StartCoroutine(CoroutineDeathEffects());        
    }
    IEnumerator CoroutineDeathEffects()
    {
        mainSprite.enabled = false;
        cameraController.ScreenShake(deathShakeDuration, deathShakeStrength);
        getHitParticles.Play();
        hitFlashSprite.DOFade(1, secondsToFadeSprite);
        yield return new WaitForSeconds(flashDuration);
        hitFlashSprite.DOFade(0, secondsToFadeSprite);

        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
