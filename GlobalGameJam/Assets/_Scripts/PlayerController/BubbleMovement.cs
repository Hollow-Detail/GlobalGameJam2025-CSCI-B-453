using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class BubbleMovement : MonoBehaviour
{
    [SerializeField] private float maxYSpeed, maxXSpeed, gravity, acceleration, decelerationScale;
    [SerializeField] private float popDrag;
 
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = Mathf.Abs(gravity) * -1;
    }

    private void Start()
    {
        GameManager.Instance.bubblePop.OnBubblePop += OnBubblePop;
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        rb.gravityScale = 0;
        rb.linearDamping = popDrag;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -maxXSpeed, maxXSpeed),Mathf.Clamp(rb.linearVelocityY, 0, maxYSpeed));
        
        
        // DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
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
