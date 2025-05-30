using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NaughtyAttributes;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using TMPro;
using Unity.Cinemachine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public BubblePop bubblePop { get; private set; }
    [field: SerializeField] public BubbleMovement currentBubble { get; private set; }
    [field: SerializeField] public EndCutsceneTrigger endCutsceneTrigger { get; private set; }
    [SerializeField] private GameObject startGameCamera, gameOverCamera, endCutsceneCamera, startGameCanvas;
    [SerializeField] private TextMeshProUGUI distanceText, bestScoreText, newBestText;

    [SerializeField] private CinemachineCamera followCamera;
    [SerializeField] private float waitBeforeZoomTime, gameOverOrthographicSize, gameOverZoomSpeed, tweenDuration;
    [SerializeField] private Vector2 tweenOffset;
    // [Header("Height System")]
    [SerializeField] private float startHeight, maxHeight, heightScale;

    [NaughtyAttributes.ReadOnly, SerializeField] public float currentHeight, currentHeight01;
    
    private bool isGameStarted = false;
    public event EventHandler OnStartGame;
    public event EventHandler OnEndGame;

    private void Start()
    {
        bubblePop.OnBubblePop += OnBubblePop;
        endCutsceneTrigger.OnEndCutscene += OnEndCutsene;
        
        
        startGameCamera.gameObject.SetActive(true);
        bestScoreText.text = "Best: " + PlayerPrefs.GetInt("BestScore", 0) + " meters";
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && !isGameStarted)
        {
            StartGame();
        }
        
        currentHeight01 = Utilites.Map(currentBubble.transform.position.y, startHeight, maxHeight, 0, 1);
        currentHeight = currentHeight01 * heightScale;

        distanceText.text = (int)currentHeight + " meters";

    }

    
    
    public void EndGame()
    {
        OnEndGame?.Invoke(this, EventArgs.Empty);
    }

    private void OnBubblePop(object sender, EventArgs args)
    {
        StartCoroutine(DeathCamera());
    }

    private IEnumerator DeathCamera()
    {
        
        int bestScore = PlayerPrefs.GetInt("BestScore");
        if (currentHeight > bestScore)
        {
            newBestText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("BestScore", (int)currentHeight);
        }
        
        distanceText.rectTransform.DOLocalMove(tweenOffset, tweenDuration).SetEase(Ease.InOutSine);
        distanceText.rectTransform.DOScale(Vector3.one * 2.5f, tweenDuration).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(waitBeforeZoomTime);
        while (followCamera.Lens.OrthographicSize > gameOverOrthographicSize)
        {
            followCamera.Lens.OrthographicSize -= gameOverZoomSpeed * Time.deltaTime;
            yield return null;
        }
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
        followCamera.gameObject.SetActive(true);
        startGameCamera.SetActive(false);
        isGameStarted = true;
    }

}
