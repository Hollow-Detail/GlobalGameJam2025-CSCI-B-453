using UnityEngine;

public class PlayerMove : State
{
    [SerializeField] private BubbleMovement bm;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.gravityScale = Mathf.Abs(bm.gravity) * -1;
        rb.linearDamping = 0;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        // change X speed lower if in ice
        float clampXSpeed = bm.maxXSpeed;
        float clampYSpeed = bm.maxYSpeed;
        if (bm.inIce)
        {
            clampXSpeed = bm.maxXSpeedIce;
            clampYSpeed = bm.maxYSpeedIce;
        }
        // Only clamp X speed if not in a wind tunnel
        
        if (!bm.inWind)
        {
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocityX, -clampXSpeed, clampXSpeed),rb.linearVelocityY);
        }
        
        rb.linearVelocity = new Vector2(rb.linearVelocityX,Mathf.Clamp(rb.linearVelocityY, -3, clampYSpeed));
    }

    public override void DoFixedUpdateState()
    {
        base.DoFixedUpdateState();
        
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Move Bubble Horizontally
        if (playerInput.x != 0f)
        {
            rb.AddForce(new Vector2(playerInput.x * bm.acceleration, 0), ForceMode2D.Force);
        }
        // Decel Bubble when no input
        else
        {
            rb.AddForce(new Vector2(-rb.linearVelocityX * bm.decelerationScale, 0f), ForceMode2D.Force);
        }
    }
}
