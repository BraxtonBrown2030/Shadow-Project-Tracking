using UnityEngine;

public class LightMovementScript : MonoBehaviour
{
    public Vector3 resetPosition = Vector3.zero; // Position to reset to
    public float movementThreshold = 0.1f; // Minimum movement distance
    public float resetTime = 5f; // Time in seconds before resetting

    private Vector3 lastPosition;
    private float timeSinceLastMove;

    void Start()
    {
        lastPosition = transform.position;
        timeSinceLastMove = 0f;
    }

    void FixedUpdate()
    {
        // Check if the object has moved
        if (Vector3.Distance(transform.position, lastPosition) > movementThreshold)
        {
            // Reset the timer and update the last position
            timeSinceLastMove = 0f;
            lastPosition = transform.position;
        }
        else
        {
            // Increment the timer
            timeSinceLastMove += Time.deltaTime;

            // Reset position if the timer exceeds the reset time
            if (timeSinceLastMove >= resetTime)
            {
                transform.position = resetPosition;
                timeSinceLastMove = 0f; // Reset the timer
            }
        }
    }
}
