using System;
using UnityEngine;

public class IcePatch : MonoBehaviour
{
    [SerializeField] private float freezeTime;
    private float timer;
    private bool timerActive;
    private void Update()
    {
        if (!timerActive) return;
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timerActive = false;
            GameManager.Instance.currentBubble.FreezeBubble();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BubbleMovement bubbleMovement))
        {
            timer = freezeTime;
            timerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out BubbleMovement bubbleMovement))
        {
            timerActive = false;
        }
    }
    
}
