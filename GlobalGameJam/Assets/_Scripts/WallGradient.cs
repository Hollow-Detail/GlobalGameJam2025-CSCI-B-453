using UnityEngine;

public class WallGradient : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Gradient gradient;

    private void Update()
    {
        spriteRenderer.color = gradient.Evaluate(1 - GameManager.Instance.currentHeight01);
    }
}
