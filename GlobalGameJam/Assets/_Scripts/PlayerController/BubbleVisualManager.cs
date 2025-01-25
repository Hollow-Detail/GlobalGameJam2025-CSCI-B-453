using System;
using UnityEngine;
using DG.Tweening;
public class BubbleVisualManager : MonoBehaviour
{
    [SerializeField] private float squashScale, squashTime, translateScale, translateTime;
    [SerializeField] private GameObject sprite;
    [SerializeField] private ParticleSystem popParticles;

    private void Awake()
    {
        transform.DOScaleY(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
        transform.DOScaleX(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetInverted().SetLink(gameObject);
        transform.DOLocalMoveY(translateScale, translateTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.bubblePop.OnBubblePop += OnBubblePop;
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        sprite.SetActive(false);
        popParticles.Play();
    }
}
