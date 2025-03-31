import cv2 # OpenCV
from cvzone.FaceMeshModule import FaceMeshDetector # Face mesh detection module

# Web Camera
cap = cv2.VideoCapture(0) # Camera Number
cap.set(3, 1280) # Width
cap.set(4, 720) # Height

# Face Mesh Detector
detector = FaceMeshDetector(maxFaces=2)

while True:
    success, img = cap.read() # Detecting the camera and continue to read the camera if a camera is present

    # Face Mesh Detection
    img, faces = detector.findFaceMesh(img)

    # Display
    cv2.imshow("Webcam", img)
    cv2.waitKey(1)