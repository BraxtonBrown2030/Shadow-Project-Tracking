using UnityEngine;
using OpenCvSharp;
using OpenCvSharp.Demo;

public class Countor_Finder_Explanation : WebCamera
{
    // WebCamera is the OpenCV Library for Unity script that handles webcam input and processing and still inharests from monobehaviour just add more functionality.
     // Serialized fields allow modifying values in Unity's Inspector even if the variable is private.
    [SerializeField] private FlipMode imageFlip; // Flip mode for image processing
    [SerializeField] private float Thereshold = 96.4f; // Threshold value for binarization (changing ths will affect how much of the image is black and white)
    [SerializeField] private bool showprossesingimage = true; // Toggle to show processed image (change between black and white or normal)
    [SerializeField] private float CurveAccuracy = 10f; // Approximation accuracy for contour curves
    [SerializeField] private float minArea = 5000f; // Minimum contour area to be considered (what is begining detected)
    [SerializeField] private PolygonCollider2D polygonCollider; // Collider for detected contours (object collision)
    
    private Mat image; // Original image (Mat is the opencv image format) (normal image)
    private Mat prossesedImage = new Mat(); // Processed image (black and white image)
    private Point[][] countours; // Array to store found contours (point for edges)
    private HierarchyIndex[] hierarchy; // Hierarchy information for contours (note sure what this is yet)
    private Vector2[] vectorlist; // Stores converted contour points (corners of the shape)
    
    // Overrides WebCamera's ProcessTexture function
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        // Convert WebCamTexture to OpenCV Mat format
        image = OpenCvSharp.Unity.TextureToMat(input);
        
        // Flip the image according to the specified mode
        Cv2.Flip(image, image , imageFlip);
        
        // Convert the image to grayscale
        Cv2.CvtColor(image, prossesedImage, ColorConversionCodes.BGR2GRAY);
        
        // Apply thresholding to create a binary image
        Cv2.Threshold(prossesedImage, prossesedImage, Thereshold, 255, ThresholdTypes.BinaryInv);
        
        // Find contours in the thresholded image
        Cv2.FindContours(prossesedImage, out countours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);

        // Reset polygon collider paths
        polygonCollider.pathCount = 0;
        
        foreach (Point[] countour in countours)
        {
            // Approximate contour with a simpler polygon
            Point[] points = Cv2.ApproxPolyDP(countour, CurveAccuracy, true);
            var area = Cv2.ContourArea(countour);

            // Only process contours with an area larger than minArea
            if (area > minArea)
            {
                // Draw detected contour for visualization
                Drawcontours(prossesedImage, new Scalar(127,127,127), 2, points);
                
                // Add the contour as a path in the polygon collider
                polygonCollider.pathCount++;
                polygonCollider.SetPath(polygonCollider.pathCount - 1, toVector2s(points));
            }
        }

        // Convert processed image to a texture and update output
        if (output == null)
            output = OpenCvSharp.Unity.MatToTexture(showprossesingimage ? prossesedImage : image);
        else
            OpenCvSharp.Unity.MatToTexture(showprossesingimage ? prossesedImage : image, output);

        return true;
    }
    
    // Converts an array of OpenCV Point to Unity's Vector2 array
    private Vector2[] toVector2s(Point[] points)
    {
        vectorlist = new Vector2[points.Length];
        
        for(int i = 0; i < points.Length; i++)
        {
            vectorlist[i] = new Vector2((float)points[i].X, (float)points[i].Y);
        }

        return vectorlist;
    }
    
    // Draws a contour by connecting its points with lines
    private void Drawcontours(Mat image, Scalar color, int thickness, Point[] points)
    {
        for (int i = 1; i < points.Length; i++)
        {
            Cv2.Line(image, points[i - 1], points[i], color, thickness);
        }
        // Connect the last point to the first to close the shape
        Cv2.Line(image, points[points.Length - 1], points[0], color, thickness);
    }
}
