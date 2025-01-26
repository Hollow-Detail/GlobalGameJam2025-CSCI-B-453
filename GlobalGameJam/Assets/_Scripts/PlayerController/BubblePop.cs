using System;
using UnityEngine;

public class BubblePop : MonoBehaviour
{
    public event EventHandler OnBubblePop;
    [SerializeField] private bool isInvincible;
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IPopper popper))
        {
            popper.Pop();
            if(!isInvincible)
                OnBubblePop?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent(out IPopper popper))
        {
            popper.Pop();
            if(!isInvincible)
                OnBubblePop?.Invoke(this, EventArgs.Empty);
        }
    }
}
