using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class BubbleMovement : StateMachineCore
{
    [Header("States")] 
    [field:SerializeField] public State moveState { get; private set; }
    [field:SerializeField] public State frozenState { get; private set; }
    [field:SerializeField] public State idleState { get; private set; }

    [Header("Bubble Movement Variables")]
    [SerializeField] public float popDrag;
    [SerializeField] public float maxYSpeed, maxYSpeedIce, maxXSpeed, maxXSpeedIce, gravity, frozenGravity, acceleration, decelerationScale;
    private float freezeTimer;
    public bool inWind, inIce;
    void Awake()
    {
        SetupInstances();
        stateMachine.SetState(idleState);
    }

    private void Start()
    {
        // TODO: Clean up subscriptions
        GameManager.Instance.bubblePop.OnBubblePop += OnBubblePop;
        GameManager.Instance.OnStartGame += OnStartGame;

    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        rb.gravityScale = 0;
        rb.linearDamping = popDrag;
        SoundManager.Instance?.PlayEntireSound(SoundManager.Sounds.Pop);
        stateMachine.SetState(idleState);
    }
    
    private void OnStartGame(object sender, EventArgs args)
    {
        stateMachine.SetState(moveState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState?.DoUpdateBranch();
        
        if (stateMachine.currentState == frozenState && stateMachine.currentState.isComplete)
        {
            stateMachine.SetState(moveState);
        }
        
        // DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    private void FixedUpdate()
    {
        stateMachine.currentState?.DoFixedUpdateBranch();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Wind wind))
        {
            inWind = true;
        }
        
        if (other.TryGetComponent(out IcePatch icePatch))
        {
            inIce = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Wind wind))
        {
            inWind = false;
        }
        
        if (other.TryGetComponent(out IcePatch icePatch))
        {
            inIce = false;
        }
    }

    public void FreezeBubble()
    {
        stateMachine.SetState(frozenState);
    }
}
