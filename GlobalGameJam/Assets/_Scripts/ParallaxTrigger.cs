using System;
using UnityEngine;

/// <summary>
/// Make sure there is a collider on this object that is marked as a trigger
/// This will enable a disabled game object once the trigger is entered 
/// </summary>

public class ParallaxTrigger : MonoBehaviour
{
    [SerializeField] private Parallax[] parallaxObjects;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Parallax parallax in parallaxObjects)
            {
                parallax.enabled = true;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}