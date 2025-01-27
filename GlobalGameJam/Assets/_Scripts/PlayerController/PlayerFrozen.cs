using UnityEngine;

public class PlayerFrozen : State
{
    [SerializeField] private BubbleMovement bm;
    [SerializeField] private float maxFallSpeed, frozenDuration;
    [SerializeField] private Sprite frozenSprite, regularSprite;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ParticleSystem thawParticles;
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Debug.Log("PlayerFrozen");
        rb.gravityScale = bm.frozenGravity;
        sr.sprite = frozenSprite;
    }

    public override void DoUpdateState()
    {
        base.DoUpdateState();
        if (stateUptime >= frozenDuration)
        {
            isComplete = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        sr.sprite = regularSprite;
        thawParticles.Play();
        SoundManager.Instance?.PlayEntireSound(SoundManager.Sounds.Thaw);
    }
}
