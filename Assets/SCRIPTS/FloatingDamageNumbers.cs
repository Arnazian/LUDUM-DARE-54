using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class FloatingDamageNumbers : MonoBehaviour
{
    [SerializeField] private Transform numberTransform;
    [SerializeField] private TextMeshProUGUI numberText;

    [SerializeField] private float moveUpAmount;
    [SerializeField] private float timeToMoveUpInSeconds;
    [SerializeField] private float timeToFadeInSeconds;

    private Vector2 originalPosition;
    private float originalAlpha;

    private void Awake()
    {
        originalAlpha = numberText.color.a;
        numberText.DOFade(0f, 0f);
        originalPosition = numberTransform.localPosition;
    }

    private void Update()
    {
        Debug.Log("Remove this");
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartMoveUp("100");
        }
    }
    public void StartMoveUp(string number)
    {
        float newYPosition = originalPosition.y + moveUpAmount;
        numberText.text = number;
        numberTransform.localPosition = originalPosition;
        numberText.DOFade(originalAlpha, 0.2f);
        numberTransform.DOLocalMoveY(newYPosition, timeToMoveUpInSeconds);
        StartCoroutine(CoroutineStartMoveUp());
    }

    IEnumerator CoroutineStartMoveUp()
    { 
        yield return new WaitForSeconds(timeToMoveUpInSeconds / 2);
        numberText.DOFade(0, timeToFadeInSeconds);
    }

}
