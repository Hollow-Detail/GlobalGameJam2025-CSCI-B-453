using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] private float maxYSpeed, gravity, acceleration, decelerationScale;

    [SerializeField] private float squashScale, squashTime;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Mathf.Abs(gravity) * -1;
    }
    void Start()
    {
        transform.DOScaleY(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DOScaleX(squashScale, squashTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo).SetInverted();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX,Mathf.Clamp(rb.linearVelocityY, 0, maxYSpeed));
    }

    void FixedUpdate()
    {
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        
        // Move Bubble Horizontally
        if (playerInput.x != 0f)
        {
            rb.AddForce(new Vector2(playerInput.x * acceleration, 0), ForceMode2D.Force);
        }
        // Decel Bubble when no input
        else
        {
            rb.AddForce(new Vector2(-rb.linearVelocityX * decelerationScale, 0f), ForceMode2D.Force);
        }
    }
    
}
