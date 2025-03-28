using UnityEngine;
using System.Collections;

public class IcicleShakeAndFall : MonoBehaviour
{
    [Header("Settings")]
    public float shakeDuration = 0.5f; // Duration of the shake
    public float shakeMagnitude = 0.1f; // Magnitude of the shake
    public float fallSpeed = 5f; // Speed at which the icicle falls
    public float raycastDistance = 1f; // Distance for raycast to check for player

    private bool isShaking = false;
    private bool isFalling = false;
    private Vector3 originalPosition;
    private Rigidbody2D rb;

    void Start()
    {
        originalPosition = transform.position; // Store the original position of the icicle
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component to handle the falling
    }

    void Update()
    {
        if (!isShaking && !isFalling)
        {
            // Perform a raycast downward to check if the player is beneath the icicle
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastDistance);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // Player is beneath the icicle, start the shake and fall process
                StartCoroutine(ShakeAndFall());
            }
        }
    }

    private IEnumerator ShakeAndFall()
    {
        isShaking = true;

        // Shake the icicle for the specified duration
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            // Shake in random directions within the specified magnitude
            float shakeX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float shakeY = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.position = new Vector3(originalPosition.x + shakeX, originalPosition.y + shakeY, originalPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // After shaking, reset the position to the original position
        transform.position = originalPosition;

        // Start falling
        isShaking = false;
        isFalling = true;
        
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the editor (only for debugging)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * raycastDistance);
    }
}
