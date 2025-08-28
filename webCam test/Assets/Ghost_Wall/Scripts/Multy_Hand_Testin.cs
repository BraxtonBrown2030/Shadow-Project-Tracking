using UnityEngine;

public class Multy_Hand_Testin : MonoBehaviour
{
    [TextArea(3, 10)] public string playerLightMovementNotes =
        "This script will receive data from the UDPReceive script and move multiple player lights GameObjects " +
        "to the positions specified by the data. The data is expected to be in the format '[x1,y1,x2,y2,...]'. " +
        "The x values are adjusted by subtracting 7 to match Unity's coordinate system. " +
        "If no new data is received within a specified threshold, the player lights will be set to default positions.";

    [Tooltip("This is the UDPReceive script that contains the data from the UDP connection.")]
    public UDPReceive udpReceive;

    [Tooltip("These are the GameObjects that will be moved to the positions of the player lights." +
             "the number of light can be changed by pressing E when the App is running It will ad that many light to the game.")]
    public GameObject[] playerLights;

    [Tooltip("Distance from the player lights to the camera. Adjust as needed.")]
    public float zDistance = -9.94f;

    [Tooltip("Stores the last time the lights were moved to new positions." +
             "If the lights have not been moved for a certain amount of time, they will be set to default positions.")]
    private string previousData = ""; // Store the previous data

    [Tooltip("Time since the last update to check for stale data.")]
    private float timeSinceLastUpdate = 0f; // Track time since the last update

    [Tooltip("Time in seconds before considering data stale and resetting the light positions.")]
    public float updateThreshold = 1f; // Time in seconds before considering data stale

    void FixedUpdate()
    {
        string data = udpReceive.data; // Get the latest data from UDPReceive

        // Check if the data is the same as the previous data
        if (data == previousData)
        {
            timeSinceLastUpdate += Time.fixedDeltaTime;

            // Reset lights to default positions if data is stale
            if (timeSinceLastUpdate >= updateThreshold)
            {
                for (int i = 0; i < playerLights.Length; i++)
                {
                    playerLights[i].transform.localPosition = new Vector3(0, -12f, zDistance); // Default position
                }
            }
            return; // Exit if data hasn't changed
        }

        // Reset the timer and process new data
        timeSinceLastUpdate = 0f;

        // Work with a copy of the data
        string[] values = data.Split(',');

        // Calculate the number of iterations (half the values length)
        int iterations = Mathf.Min(playerLights.Length, values.Length / 2);

        // Update lights with valid data
        for (int i = 0; i < iterations; i++)
        {
            float x = 7 - float.Parse(values[i * 2]) / 100;
            float y = float.Parse(values[i * 2 + 1]) / 100;

            playerLights[i].transform.localPosition = new Vector3(x, y, zDistance);
        }

        // Reset remaining lights to a default position
        for (int i = iterations; i < playerLights.Length; i++)
        {
            playerLights[i].transform.localPosition = new Vector3(0, -12f, zDistance); // Default position
        }

        // Update previousData after processing
        previousData = data;
    }
}    