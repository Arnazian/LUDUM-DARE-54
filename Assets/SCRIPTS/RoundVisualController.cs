using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using static UnityEngine.CullingGroup;


public class RoundVisualController : MonoBehaviour
{
    [SerializeField] private Vector2 changeScale;
    [SerializeField] private float scaleDuration;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private Transform roundTransform;
    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = roundTransform.localScale;
    }

    private void Start()
    {
        GameSession.OnStateChanged += ChangeRoundNumber;
    }

    public void ChangeRoundNumber(GameSession.State state)
    {
        if (state != GameSession.State.COMBAT) return;

        roundText.text = GameSession.EncounterCounter.ToString();
        roundTransform.DOScale(changeScale, scaleDuration).OnComplete(() =>
        {
            roundTransform.DOScale(originalScale, scaleDuration);
        });
    }
}
