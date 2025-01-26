using UnityEngine;
using System;
public class EndCutsceneTrigger : MonoBehaviour
{
    public event EventHandler OnEndCutscene;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnEndCutscene?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
