using UnityEngine;

public class HandTrakingInformation : MonoBehaviour
{
        
    [Tooltip("This is the component need the UDPReceive Script (just put the UDPReceive script here).")]
    public UDPReceive udpReceive;

    [Tooltip("HandPoints is the array of GameObjects that will be moved to the position of the hand points. Keep in mind that there need to be 21 objects in the array to track the hand properly.")]
    public GameObject[] handPoints;
    [Tooltip("OtherHandPoints is the array of GameObjects that will be moved to the position of the hand points. Keep in mind that there need to be 21 objects in the array to track the hand properly.")]
    public GameObject[] otherHandPoints;

    [TextArea(5, 20)] public string Hand_Tracking_Notes =
        "This script will receive data from the UDPReceive script in the form of a string list." +
        "the put the string into a new string called data then grab the reverent information " +
        "and landmark points to position the GameObjects on to approach position. " +
        "It then runs the for loop to grab the parts of the string then sets each GameObject" +
        "to x y and z position of the landmarks then flips them in relation to python." +
        "The landmarks positions data is reversed in the x axis to match the unity world space.";

    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        print(data);
        string[] points = data.Split(',');
        print(points[0]);

        // Ensure there are enough points
        if (points.Length < 126)
        {
            Debug.LogError("Not enough points received.");
            return;
        }

        // Process the first hand points
        for (int i = 0; i < 21; i++)
        {
            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
        }

        // Process the other hand points
        for (int i = 21; i < 42; i++)
        {
            float x = 7 - float.Parse(points[i * 3]) / 100;
            float y = float.Parse(points[i * 3 + 1]) / 100;
            float z = float.Parse(points[i * 3 + 2]) / 100;

            otherHandPoints[i - 21].transform.localPosition = new Vector3(x, y, z);
        }
    }
}