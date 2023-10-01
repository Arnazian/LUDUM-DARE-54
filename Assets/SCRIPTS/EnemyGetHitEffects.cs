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

    void Start()
    {
        hitFlashSprite.DOFade(0, 0);
    }

    private void Update()
    {
        /// deleeete
        if(Input.GetKeyDown(KeyCode.J))
        {
            DoGetHitEffects();
        }
    }

    public void DoGetHitEffects()
    {
        // move to card if we have different sounds for each different card
        audioSource.clip = getHitClip;
        audioSource.Play();

        StartCoroutine(CoroutineHitFlash());       
    }

    IEnumerator CoroutineHitFlash()
    {
        getHitParticles.Play();
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
        // replace with death effects
        DoGetHitEffects();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
