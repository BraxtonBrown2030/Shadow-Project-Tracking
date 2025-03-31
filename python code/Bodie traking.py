import cv2
from cvzone.PoseModule import PoseDetector
import socket

# Initialize webcam
cap = cv2.VideoCapture(0)
cap.set(3, 1280)  # Width
cap.set(4, 720)   # Height

# Initialize Pose Detector
detector = PoseDetector()

socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5052) 

while True:
    success, img = cap.read()
    if not success:
        break

    # Find the pose and draw landmarks
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img, bboxWithHands=True)

    if lmList:
        # Example: Get the coordinates of the nose (landmark 0)
        nose = lmList[0]
        print(f"Nose coordinates: {nose}")

    # Display the image
    cv2.imshow("Image", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()