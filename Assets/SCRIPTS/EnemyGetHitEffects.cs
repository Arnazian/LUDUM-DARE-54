using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyGetHitEffects : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer mainSprite;
    [SerializeField] private SpriteRenderer hitFlashSprite;

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
}
