using UnityEngine;

public class PlayerEndCutscene : State
{
    [SerializeField] private float gravity, centerSpeed, maxSpeedY;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        rb.position = new Vector2(Mathf.Lerp(rb.position.x, 0, centerSpeed * Time.deltaTime), rb.position.y);
        rb.linearVelocity = new Vector2(0, Mathf.Clamp(rb.linearVelocity.y, 0, maxSpeedY));
    }
}
