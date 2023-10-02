using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GhostVisuals : MonoBehaviour
{
    [SerializeField] private EnemyComponent enemyComp;
    [SerializeField] private SpriteRenderer ghostSprite;
    [SerializeField] private float transAlpha;
    [SerializeField] private float fadeDurationInSeconds;
    private float originalAlpha;
    private void Start()
    {
        Combat.OnEventLogChanged += OnEvent;
        originalAlpha = ghostSprite.color.a;
        ghostSprite.DOFade(transAlpha, fadeDurationInSeconds);
    }

    private void OnEvent(CombatEvent e)
    {
        if (e.Type != CombatEvent.EventType.StatusApplied) return;
        if (e.Target != enemyComp.enemy) return;
        if(e.Args[0] is PhasedIn) DoFadeIn();
        if(e.Args[0] is PhasedOut) DoFadeOut();
    }

    public void DoFadeIn()
    {
        ghostSprite.DOFade(originalAlpha, fadeDurationInSeconds);
    }
    public void DoFadeOut()
    {
        StartCoroutine(CoroutineFadeOutWithDelay());
    }

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnEvent;
    }

    IEnumerator CoroutineFadeOutWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        ghostSprite.DOFade(transAlpha, fadeDurationInSeconds);

    }

}

