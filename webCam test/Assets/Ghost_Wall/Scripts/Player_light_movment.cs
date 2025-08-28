using UnityEngine;

public class Player_light_movment : MonoBehaviour
{
    [TextArea(3, 10)] public string playerLightMovementNotes =
        "This script will receive data from the UDPReceive script and move the player lights GameObject " +
        "to the position specified by the data. The data is expected to be in the format '[x,y]'. " +
        "The x value is adjusted by subtracting 7 to match Unity's coordinate system. " +
        "If no new data is received within a specified threshold, the player lights will be set to a default position.";
    
    [Tooltip("This is the UDPReceive script that contains the data from the UDP connection.")]
    public UDPReceive udpReceive;
    
    [Tooltip("This is the GameObject that will be moved to the position of the player lights.")]
    public GameObject playerLights;
    
    [Tooltip("Distance from the player lights to the camera. Adjust as needed.")]
    public float zDistance = -9.94f;
    
    [Tooltip("stores the last time the light was moved to a new position. " +
             "If the light has not been moved for a certain amount of time, it will be set to a default position.")]
    private string previousData = ""; // Store the previous data
    
    [Tooltip("Time since the last update to check for stale data.")]
    private float timeSinceLastUpdate = 0f; // Track time since the last update
    
    [Tooltip("Time in seconds before considering data stale and resetting the light position.")]
    public float updateThreshold = 1f; // Time in seconds before considering data stale

    void FixedUpdate()
    {
        string data = udpReceive.data;

        // Check if data has changed
        if (data != previousData)
        {
            previousData = data; // Update the previous data
            timeSinceLastUpdate = 0f; // Reset the timer

            // Process the data
            data = data.Remove(0, 1);
            data = data.Remove(data.Length - 1, 1);
            string[] values = data.Split(',');

            if (values.Length >= 2)
            {
                float x = 7 - float.Parse(values[0]) / 100;
                float y = float.Parse(values[1]) / 100;

                playerLights.transform.localPosition = new Vector3(x, y, zDistance);
            }
        }
        else
        {
            // Increment the timer if data hasn't changed
            timeSinceLastUpdate += Time.deltaTime;

            // Check if the data is stale
            if (timeSinceLastUpdate > updateThreshold)
            {
                // Set the light to a default position
                playerLights.transform.localPosition = new Vector3(0, -12f, zDistance);
            }
        }
    }
}