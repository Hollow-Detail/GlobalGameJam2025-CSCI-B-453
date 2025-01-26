using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            onTriggerEnter?.Invoke();
    }
}
