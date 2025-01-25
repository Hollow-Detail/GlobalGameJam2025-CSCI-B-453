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
            
            GameManager.Instance.currentBubble.FreezeBubble();
            timerActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer = freezeTime;
            timerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timerActive = false;
        }
    }
    
}
