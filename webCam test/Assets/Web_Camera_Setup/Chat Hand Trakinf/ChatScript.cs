using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;

public class ChatScript : MonoBehaviour
{
    public RawImage display;
    private WebCamTexture webCamTexture;
    private Mat frame;
    private Mat hsvFrame;
    private Mat mask;
    private Scalar lowerBound = new Scalar(0, 30, 60); // Lower bound for skin color in HSV
    private Scalar upperBound = new Scalar(20, 150, 255); // Upper bound for skin color in HSV

    void Start()
    {
        webCamTexture = new WebCamTexture();
        display.texture = webCamTexture;
        webCamTexture.Play();
    }

    void Update()
    {
        if (webCamTexture.didUpdateThisFrame)
        {
            
            frame = OpenCvSharp.Unity.TextureToMat(webCamTexture);
            ProcessFrame(frame);
            display.texture = OpenCvSharp.Unity.MatToTexture(frame);
        }
    }

    void ProcessFrame(Mat frame)
    {
        // Convert the frame to HSV color space
        hsvFrame = new Mat();
        Cv2.CvtColor(frame, hsvFrame, ColorConversionCodes.BGR2HSV);

        // Create a mask for skin color
        mask = new Mat();
        Cv2.InRange(hsvFrame, lowerBound, upperBound, mask);

        // Find contours in the mask
        Point[][] contours;
        HierarchyIndex[] hierarchy;
        Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

        // Draw contours on the frame
        foreach (var contour in contours)
        {
            Cv2.DrawContours(frame, new[] { contour }, -1, new Scalar(0, 255, 0), 2);
        }
    }
}