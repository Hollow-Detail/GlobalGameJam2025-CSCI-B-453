using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class CinematicBars : MonoBehaviour
{
    [SerializeField] private Transform bubble;


    private void Update()
    {
        transform.position = new Vector2(transform.position.x, bubble.position.y);

    }

    
    
    
}
