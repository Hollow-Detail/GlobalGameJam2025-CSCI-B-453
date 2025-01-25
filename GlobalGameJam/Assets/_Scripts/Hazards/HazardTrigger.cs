using System;
using UnityEngine;

/// <summary>
/// Make sure there is a collider on this object that is marked as a trigger
/// This will enable a disabled game object once the trigger is entered 
/// </summary>

public class HazardTrigger : MonoBehaviour
{
    [SerializeField] private GameObject hazardToEnable;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hazardToEnable.SetActive(true);
        }
    }
}
