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
    [field:SerializeField] public State endCutsceneState { get; private set; }

    [Header("Bubble Movement Variables")]
    [SerializeField] public float popDrag;
    [SerializeField] public float maxYSpeed, maxYSpeedIce, maxXSpeed, maxXSpeedIce, gravity, frozenGravity, acceleration, decelerationScale;
    [SerializeField] private Material bubbleMaterial;
    private float freezeTimer;
    public bool inWind, inIce;

    private BubbleHeat bubbleHeat;
    void Awake()
    {
        SetupInstances();
        stateMachine.SetState(idleState);

        // grabbing the value from the bubble
        bubbleHeat = GetComponent<BubbleHeat>();
    }

    private void Start()
    {
        // TODO: Clean up subscriptions
        GameManager.Instance.bubblePop.OnBubblePop += OnBubblePop;
        GameManager.Instance.OnStartGame += OnStartGame;
        GameManager.Instance.endCutsceneTrigger.OnEndCutscene += OnEndCutscene;
    }

    public void OnEndGame()
    {
        stateMachine.SetState(idleState);
        rb.gravityScale = 0;
        rb.linearDamping = popDrag / 5f;
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        rb.gravityScale = 0;
        rb.linearDamping = popDrag;
        SoundManager.Instance?.PlayEntireSound(SoundManager.Sounds.Pop);
        stateMachine.SetState(idleState);
        Destroy(GetComponent<Collider2D>());
    }
    
    private void OnStartGame(object sender, EventArgs args)
    {
        stateMachine.SetState(moveState);
    }

    private void OnEndCutscene(object sender, EventArgs args)
    {
        stateMachine.SetState(endCutsceneState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState?.DoUpdateBranch();
        
        if (stateMachine.currentState == frozenState && stateMachine.currentState.isComplete)
        {
            stateMachine.SetState(moveState);
        }

        if (bubbleHeat.heatValue > 25)
        {
            IncreaseSpeed();
        }
        if (bubbleHeat.heatValue < 20)
        {
            DecreaseSpeed();
        }
        if(bubbleHeat.heatValue < 5)
        {
            inIce = true;
        }
        if (inIce)
        {
            float _frozenAmount =  bubbleMaterial.GetFloat("_FrozenAmount" );
            
            bubbleMaterial.SetFloat("_FrozenAmount", Mathf.Clamp01(_frozenAmount + Time.deltaTime * 2));
        }
        else
        {
            bubbleMaterial.SetFloat("_FrozenAmount", 0);
        }
        
        // DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    // Increases the speed
    public void IncreaseSpeed()
    {
        float normalizedHeat = Mathf.Clamp01(bubbleHeat.heatValue / 100f); 
        
        float horizontalSpeedMultiplier = 1f + (normalizedHeat * 0.9f); 
        
        maxXSpeed = Mathf.Lerp(0, maxYSpeed, horizontalSpeedMultiplier);
    }

    // Decrease the speed 

    public void DecreaseSpeed()
    {
        float noramlizedheat2 = Mathf.Clamp01(bubbleHeat.heatValue / 100f);

        float horizontalSpeedMultiplier2 = 1f + (noramlizedheat2 * 0.9f);

        maxXSpeed = Mathf.Lerp(0, maxYSpeed, horizontalSpeedMultiplier2);
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
