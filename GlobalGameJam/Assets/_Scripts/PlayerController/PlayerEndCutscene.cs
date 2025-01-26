using UnityEngine;

public class PlayerEndCutscene : State
{
    [SerializeField] private float gravity;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        rb.linearVelocity = Vector2.zero;
    }
}
