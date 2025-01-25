using UnityEngine;

/// <summary>
/// Moves this hazard in the foward direction by its speed
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class LinearMoveHazard : MonoBehaviour, IPopper
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        rb.linearVelocity = transform.right * speed;
    }
    
    
    /// <summary>
    /// Implement POP if we want a custom animation or event when the bubble is popped on this object
    /// </summary>
    public void Pop()
    {
        
    }
}
