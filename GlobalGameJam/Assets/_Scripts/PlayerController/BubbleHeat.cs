using UnityEngine;

public class BubbleHeat : MonoBehaviour
{
    // Heat Rates
    public float heatValue = 22f;
    public float heatIncrease = 3f;
    public float coldDecrease = 5f;

    // Detection Variables
    public float detectionRange = 5f;
    public Vector2 detectionSize = new Vector2(5f, 5f);  

    // Timer to reduce tick rate
    private float heat_timer = 0f;
    public float tickrate = 3f; 

    public void Update()
    {
        // Increase the timer based on the time that has passed
        heat_timer += Time.deltaTime;

        // Stop heat spam
        if (heat_timer >= tickrate)
        {
            
            heat_timer = 0f;

            // I was using the wrong collider at first. . . .
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, detectionSize, 0f);  

            // Debug colliders
            if (colliders.Length > 0)
            {
                Debug.Log("Detected " + colliders.Length + " colliders.");
            }
            else
            {
                Debug.Log("No colliders detected.");
            }

            foreach (Collider2D collider in colliders)
            {
                // Debugging tag match
                Debug.Log("Checking collider: " + collider.gameObject.name + " with tag: " + collider.tag);

                // Decrease or Increase Heat value based on values
                if (collider.CompareTag("Heat"))
                {
                    heatValue += heatIncrease;  
                    Debug.Log("Heat detected: " + collider.gameObject.name);
                }
                else if (collider.CompareTag("Cold"))
                {
                    heatValue -= coldDecrease;  
                    Debug.Log("Cold detected: " + collider.gameObject.name);
                }
                else
                {
                    Debug.Log("Collider with untagged object detected: " + collider.gameObject.name);
                }
            }

            
            Debug.Log("Current Heat Value: " + heatValue);
        }
    }

   
    
}
