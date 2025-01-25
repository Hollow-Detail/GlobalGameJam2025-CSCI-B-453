using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CinematicBars : MonoBehaviour
{
    public bool barsActive {get; private set;}
    [SerializeField] private Transform leftBar;
    [SerializeField] private Transform rightBar;
    [SerializeField] private float leftBarInactivePos;
    [SerializeField] private float leftBarActivePos;
    [SerializeField] private float rightBarInactivePos;
    [SerializeField] private float rightBarActivePos;
    [SerializeField] private float tweenTime;
    [SerializeField] private Ease ease;
    [SerializeField] private Transform bubble;


    private void Update()
    {
        transform.position = new Vector2(transform.position.x, bubble.position.y);
        // Debug for now
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleBars();       
        }
    }

    public void ToggleBars()
    {
        if (barsActive)
        {
            DeactivateBars();
        }
        else
        {
            ActivateBars();
        }
    }

    public void ActivateBars()
    {
        barsActive = true;
        leftBar.DOMoveX(leftBarActivePos, tweenTime).SetEase(ease);
        rightBar.DOMoveX(rightBarActivePos, tweenTime).SetEase(ease);
    }

    public void DeactivateBars()
    {
        barsActive = false;
        leftBar.DOMoveX(leftBarInactivePos, tweenTime).SetEase(ease);
        rightBar.DOMoveX(rightBarInactivePos, tweenTime).SetEase(ease);
    }
    
    
}
