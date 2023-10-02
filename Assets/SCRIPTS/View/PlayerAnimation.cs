using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
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
    [SerializeField] private float shakeDuration;
    [SerializeField] private float shakeStrength;

    [Header("Animator Reference")]
    [SerializeField] private Animator anim;
    private ControlSelectionRotator selectionRotator;
    private FloatingDamageNumbers floatingDamageNumbers;

    void Start()
    {
        floatingDamageNumbers = GetComponent<FloatingDamageNumbers>();
        selectionRotator = GetComponent<ControlSelectionRotator>(); 
        audioSource = GetComponent<AudioSource>();
        Combat.OnEventLogChanged += OnEvent;
    }

    private void OnEvent(CombatEvent e)
    {
        if (e.Target != GameSession.Player) return;
        e.Accept();
        switch (e.Type)
        {
            case CombatEvent.EventType.TurnStarted:
                StartCoroutine(TurnStartedAnimation(e));
                break;
            case CombatEvent.EventType.TurnEnded:
                StartCoroutine(TurnEndedAnimation(e));
                break;
            case CombatEvent.EventType.StatusApplied:
                StartCoroutine(StatusAppliedAnimation(e));
                break;
            case CombatEvent.EventType.StatusRemoved:
                StartCoroutine(StatusRemovedAnimation(e));
                break;
            case CombatEvent.EventType.Damaged:
                StartCoroutine(BeenDamagedAnimation(e));
                break;
            case CombatEvent.EventType.Healed:
                StartCoroutine(BeenHealedAnimation(e));
                break;
            case CombatEvent.EventType.Killed:
                StartCoroutine(KilledAnimation(e));
                break;
            case CombatEvent.EventType.TakenAction:
                StartCoroutine(TakenActionAnimation(e));
                break;
        }

    }

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnEvent;
    }

    IEnumerator TurnStartedAnimation(CombatEvent e)
    {
        yield return null;
        selectionRotator.EnableRotatorVisuals();
        e.Consume();
    }

    IEnumerator TurnEndedAnimation(CombatEvent e)
    {
        yield return null;
        selectionRotator.DisableRotatorVisuals();
        e.Consume();
    }



    IEnumerator StatusAppliedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator StatusRemovedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator BeenDamagedAnimation(CombatEvent e)
    {
        int amount = (int)e.Args[1];
        if (amount > 0)
        {
            floatingDamageNumbers.StartMoveUp(amount.ToString());
            audioSource.clip = getHitClip;
            audioSource.Play();
            float effectDuration = 0.5f;
            Camera.main.GetComponent<CameraController>().ScreenShake(shakeDuration, shakeStrength);
            getHitParticles.Play();
            hitFlashSprite.DOFade(1, secondsToFadeSprite);
            mainSprite.DOFade(0, secondsToFadeSprite);
            yield return new WaitForSeconds(flashDuration);
            hitFlashSprite.DOFade(0, secondsToFadeSprite);
            mainSprite.DOFade(1, secondsToFadeSprite);
            yield return new WaitForSeconds(effectDuration);
        }
        else
        {
            GetComponent<DoShieldVisuals>().StartShieldVisuals();
        }
        e.Consume();
    }

    IEnumerator BeenHealedAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator KilledAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

    IEnumerator TakenActionAnimation(CombatEvent e)
    {
        yield return null;
        e.Consume();
    }

}
