using System;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private bool isRight = true;
    [SerializeField] private float windSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out BubbleMovement bubbleMovement))
        {
            SoundManager.Instance?.PlayEntireSound(SoundManager.Sounds.Wind);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out BubbleMovement bubbleMovement))
        {
            Vector2 direction = Vector2.left;
            
            if(isRight)
                direction = Vector2.right;
            
            bubbleMovement.rb.AddForce(direction * windSpeed, ForceMode2D.Force);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
}
