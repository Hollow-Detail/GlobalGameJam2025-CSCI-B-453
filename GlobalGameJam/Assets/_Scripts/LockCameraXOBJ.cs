using System;
using UnityEngine;


public class LockCameraXOBJ : MonoBehaviour
{
    [SerializeField] private Transform bubble;

    private void Update()
    {
        transform.position = new Vector2(transform.position.x, bubble.position.y);
    }
}