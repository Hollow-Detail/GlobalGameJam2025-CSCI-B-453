using System;
using System.ComponentModel;
using NaughtyAttributes;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public BubblePop bubblePop { get; private set; }
    [field: SerializeField] public BubbleMovement currentBubble { get; private set; }
    [field: SerializeField] public EndCutsceneTrigger endCutsceneTrigger { get; private set; }
    [SerializeField] private GameObject startGameCamera, gameOverCamera, followCamera, endCutsceneCamera, startGameCanvas;
    // [Header("Height System")]
    [SerializeField] private float startHeight, maxHeight, heightScale;

    [NaughtyAttributes.ReadOnly, SerializeField] private float currentHeight, currentHeight01;
    
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
        
        currentHeight01 = Utilites.Map(currentBubble.transform.position.y, startHeight, maxHeight, 0, 1);
        currentHeight = currentHeight01 * heightScale;
        
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        Invoke(nameof(ResetGame), 1f);
        startGameCamera.gameObject.SetActive(true);
        
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
