using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public BubblePop bubblePop { get; private set; }
    [field: SerializeField] public BubbleMovement currentBubble { get; private set; }
    [SerializeField] private GameObject startGameCamera, gameOverCamera, followCamera, startGameCanvas;
    
    public event EventHandler OnStartGame;

    private void Start()
    {
        bubblePop.OnBubblePop += OnBubblePop;
        startGameCamera.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            StartGame();
        }
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        // gameOverCamera.SetActive(true);
    }

    private void StartGame()
    {
        OnStartGame?.Invoke(this, EventArgs.Empty);
        startGameCanvas.gameObject.SetActive(false);
        followCamera.SetActive(true);
        startGameCamera.SetActive(false);
    }

}
