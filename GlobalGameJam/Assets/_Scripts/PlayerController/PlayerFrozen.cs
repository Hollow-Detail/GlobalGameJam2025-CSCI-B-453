using UnityEngine;

public class PlayerFrozen : State
{
    [SerializeField] private BubbleMovement bm;
    [SerializeField] private float maxFallSpeed, frozenDuration;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("PlayerFrozen");
        rb.gravityScale = bm.frozenGravity;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime >= frozenDuration)
        {
            isComplete = true;
        }
    }
}
