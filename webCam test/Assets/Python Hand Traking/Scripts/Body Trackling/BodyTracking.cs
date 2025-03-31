using UnityEngine;

public class BodyTracking : MonoBehaviour
{
    public UDPBody udpBody;
    public GameObject bodyPoint; // Single GameObject to move

    private void Update()
    {
        string data = udpBody.data;
        
        data = data.Trim(new char[] { '[', ']' });
        string[] points = data.Split(',');

        // Ensure there are enough points
        if (points.Length < 2)
        {
            Debug.LogError("Not enough points received.");
            return;
        }

        // Parse the first landmark position
        float x = float.Parse(points[0]) / 100;
        float y = float.Parse(points[1]) / 100;

        // Update the position of the single GameObject
        bodyPoint.transform.localPosition = new Vector3(x, y, bodyPoint.transform.localPosition.z);
    }
}