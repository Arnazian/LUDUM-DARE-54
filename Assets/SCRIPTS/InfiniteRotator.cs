using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRotator : MonoBehaviour
{
    [SerializeField] private float rotationDuration = 2f;
    [SerializeField] private float direction;
    private Sequence rotatingSequence;
    void Start()
    {
        rotatingSequence = DOTween.Sequence();
        RotateForever();
    }
    void RotateForever()
    {
        rotatingSequence = DOTween.Sequence();
        rotatingSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -360),
        rotationDuration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear));
        rotatingSequence.OnComplete(() => { rotatingSequence.Restart(); });
    }
}
