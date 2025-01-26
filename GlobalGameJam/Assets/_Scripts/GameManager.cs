using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public BubblePop bubblePop { get; private set; }
    [field: SerializeField] public BubbleMovement currentBubble { get; private set; }
    [field: SerializeField] public EndCutsceneTrigger endCutsceneTrigger { get; private set; }
    [SerializeField] private GameObject startGameCamera, gameOverCamera, followCamera, endCutsceneCamera, startGameCanvas;
    private bool isGameStarted = false;
    public event EventHandler OnStartGame;

    private void Start()
    {
        bubblePop.OnBubblePop += OnBubblePop;
        endCutsceneTrigger.OnEndCutscene += OnEndCutsene;
        startGameCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !isGameStarted)
        {
            StartGame();
        }
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        // gameOverCamera.SetActive(true);
    }

    private void OnEndCutsene(object sender, EventArgs args)
    {
        endCutsceneCamera.gameObject.SetActive(true);
        followCamera.gameObject.SetActive(false);
    }

    private void StartGame()
    {
        OnStartGame?.Invoke(this, EventArgs.Empty);
        startGameCanvas.gameObject.SetActive(false);
        followCamera.SetActive(true);
        startGameCamera.SetActive(false);
        isGameStarted = true;
    }

}
