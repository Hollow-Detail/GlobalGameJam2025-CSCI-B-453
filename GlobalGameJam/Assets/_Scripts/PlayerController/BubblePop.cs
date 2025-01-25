using System;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    public event EventHandler OnBubblePop;
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.TryGetComponent(out IPopper popper))
        {
            popper.Pop();
            OnBubblePop?.Invoke(this, EventArgs.Empty);
        }
    }
}
