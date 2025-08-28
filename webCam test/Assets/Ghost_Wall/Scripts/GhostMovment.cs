using UnityEngine;

public class GhostMovment : MonoBehaviour
{
    [Tooltip("Speed at which the ghost moves towards the target positions")]
    public float speed = 5f;
    
    [Tooltip("Time in seconds between updates for the ghost movement")]
    public float updateInterval = 0.5f; // Time in seconds between updates
    
    [Tooltip("Scene Object that the ghost will be")]
    public GameObject ghosts;
    
    [Tooltip("Distance that the ghost has to be to the edge to change direction")]
    public float positionDistance = 0.1f; // Distance threshold to switch positions

    [Tooltip("top right corner of the ghost movement area")]
    public GameObject targetPosition;
    [Tooltip("bottom right corner of the ghost movement area")]
    public GameObject targetPosition2;
    [Tooltip("Generated position between targetPosition and targetPosition2 that that ghost will move to after reaching opposite side")]
    public Vector3 movepostion;

    [Tooltip("top left corner of the ghost movement area")]
    public GameObject targetPosition3;
    [Tooltip("bottom left corner of the ghost movement area")]
    public GameObject targetPosition4;
    [Tooltip("Generated position between targetPosition3 and targetPosition4 that that ghost will move to after reaching opposite side")]
    public Vector3 movepostion2;

    private bool movingToPosition2 = false; // Tracks the current target position

    /*
        the rest of the script is used to move the ghost between two positions
        movepostion and movepostion2, which are generated between the target positions.
        The ghost will move towards movepostion until it reaches the positionDistance threshold,
        then it will switch to movepostion2 and vice versa.
    */
    
    private void FixedUpdate()
    {
        if (!movingToPosition2)
        {
            // Move towards movepostion
            ghosts.transform.position = Vector3.MoveTowards(ghosts.transform.position, movepostion, speed * Time.deltaTime);

            // Check if the ghost has reached movepostion
            if (Vector3.Distance(ghosts.transform.position, movepostion) < positionDistance)
            {
                movingToPosition2 = true; // Switch to movepostion2
                ghosts.transform.Rotate(0, 180, 0); // Flip the cube
                movepostion2 = Vector3.Lerp(targetPosition3.transform.position, targetPosition4.transform.position, UnityEngine.Random.Range(0.0f, 1.0f));
            }
        }
        else
        {
            // Move towards movepostion2
            ghosts.transform.position = Vector3.MoveTowards(ghosts.transform.position, movepostion2, speed * Time.deltaTime);

            // Check if the ghost has reached movepostion2
            if (Vector3.Distance(ghosts.transform.position, movepostion2) < positionDistance)
            {
                movingToPosition2 = false; // Switch back to movepostion
                ghosts.transform.Rotate(0, 180, 0); // Flip the cube
                movepostion = Vector3.Lerp(targetPosition.transform.position, targetPosition2.transform.position, UnityEngine.Random.Range(0.0f, 1.0f));
            }
        }
    }
}