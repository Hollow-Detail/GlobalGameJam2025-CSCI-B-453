using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField] public BubblePop bubblePop { get; private set; }
    [field: SerializeField] public BubbleMovement currentBubble { get; private set; }

    private void Start()
    {
    }
}
