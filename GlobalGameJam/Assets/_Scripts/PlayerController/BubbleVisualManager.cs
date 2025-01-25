using System;
using UnityEngine;
using DG.Tweening;
public class BubbleVisualManager : MonoBehaviour
{
    [SerializeField] private float squashScale, squashTime, translateScale, translateTime;

    private void Awake()
    {
        transform.DOScaleY(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DOScaleX(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetInverted();
        transform.DOLocalMoveY(translateScale, translateTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
