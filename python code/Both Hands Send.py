import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

# Initialize webcam
cap = cv2.VideoCapture(0) # Open the default camera
cap.set(3, 1280)  # Set the width of the camera frame
cap.set(4, 720)  # Set the height of the camera frame

# 4k resolution (3840 x 2160)
# normal resolution (1280 x 720)

# Initialize Hand Detector with detection confidence of 0.8 and maximum of 2 hands
detector = HandDetector(detectionCon=0.8, maxHands=2)

# Initialize socket for sending data
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # Create a UDP socket
serverAddressPort = ("127.0.0.1", 5052) # Define the server address and port

while True:
    success, img = cap.read() # Capture a frame from the webcam
    if not success:
        break # If frame capture fails, exit the loop

    # Find the hands and draw landmarks on the image
    hands, img = detector.findHands(img)

    if hands:
        data = [] # Initialize an empty list to store landmark data
        for hand in hands:
            lmList = hand["lmList"] # Get the list of 21 landmark points for each hand
            for lm in lmList:
                # Append the x, y (inverted y-axis), and z coordinates of each landmark to the data list
                data.extend([lm[0], img.shape[0] - lm[1], lm[2]])

        # Send landmark data to the server
        sock.sendto(str.encode(str(data)), serverAddressPort)

    # Display the image
    cv2.imshow("Image", img)
    if cv2.waitKey(1) & 0xFF == ord('q'): # exit the loop when 'q' is pressed
        break

cap.release() # release the webcam
cv2.destroyAllWindows() # close all OpenCV windows