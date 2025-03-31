using OpenCvSharp;
using OpenCvSharp.Demo;
using UnityEngine;

public class CountorFinder : WebCamera
{
    
    [SerializeField] private FlipMode imageFlip;
    [SerializeField] private float Thereshold = 96.4f;
    [SerializeField] private bool showprossesingimage = true;
    [SerializeField] private float CurveAccuracy = 10f;
    [SerializeField] private float minArea = 5000f;
    [SerializeField] private PolygonCollider2D polygonCollider;
    
    private Mat image;
    private Mat prossesedImage = new Mat();
    private Point[][] countours;
    private HierarchyIndex[] hierarchy;
    private Vector2[] vectorlist;
    
    
    protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
    {
        image = OpenCvSharp.Unity.TextureToMat(input);
        
        Cv2.Flip(image, image , imageFlip);
        Cv2.CvtColor(image, prossesedImage, ColorConversionCodes.BGR2GRAY);
        Cv2.Threshold(prossesedImage, prossesedImage, Thereshold, 255, ThresholdTypes.BinaryInv);
        Cv2.FindContours(prossesedImage, out countours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple, null);
        
        
        
        polygonCollider.pathCount = 0;
        
        foreach (Point[] countour in countours)
        {
            Point[] points = Cv2.ApproxPolyDP(countour, CurveAccuracy, true);
            var area = Cv2.ContourArea(countour);

            if (area > minArea)
            {
                
                Drawcontours(prossesedImage, new Scalar(127,127,127), 2, points);
             
                polygonCollider.pathCount++;
                polygonCollider.SetPath(polygonCollider.pathCount - 1,toVector2s(points));
            }
        }

        if (output == null)
            output = OpenCvSharp.Unity.MatToTexture(showprossesingimage ? prossesedImage : image);
        else
            OpenCvSharp.Unity.MatToTexture(showprossesingimage ? prossesedImage : image, output);

        return true;
    }
    private Vector2[] toVector2s(Point[] points)
    {

        vectorlist = new Vector2[points.Length];
        
        for(int i = 0; i < points.Length; i++)
        {
            vectorlist[i] = new Vector2((float)points[i].X, (float)points[i].Y);
        }

        return vectorlist;
    }
    
    private void Drawcontours(Mat image, Scalar color, int thickness, Point[] points)
    {
        
        for (int i = 1; i < points.Length; i++)
        {
            Cv2.Line(image, points[i - 1] ,points[i], color, thickness);
        }
        Cv2.Line(image, points[points.Length - 1],points[0], color, thickness );
        
    }
}
