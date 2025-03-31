from cvzone.HandTrackingModule import HandDetector # Hand tracking module
import cv2 # for camera and image tracking
import socket # for Data transfer to unity

# open cv and media pipe are part of the cvzone library you will need to install it and media pipe to run this code

cap = cv2.VideoCapture(0) # get the default camera (laptops default are the inbuilt Camera so change the camera number if you are using an external camera)
cap.set(3, 1280) # Width
cap.set(4, 720) # Height
success, img = cap.read() # detecting the camera and continue to read the camera if a camera is present
h, w, _ = img.shape # get the height and width of the image
detector = HandDetector(detectionCon=0.8, maxHands=2) # Hand Detector number of hand and detection confidence

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM) # Socket for sending data to unity and socket type
serverAddressPort = ("127.0.0.1", 5052) # IP address and port number for sending data to unity

while True:
    # Get image frame
    success, img = cap.read()
    # Find the hand and its landmarks
    hands, img = detector.findHands(img)  # with draw
    # hands = detector.findHands(img, draw=False)  # without draw
    data = []

    if hands:
        # Hand 1
        hand = hands[0]
        lmList = hand["lmList"]  # List of 21 Landmark points per hand
        for lm in lmList:
            data.extend([lm[0], h - lm[1], lm[2]])

        sock.sendto(str.encode(str(data)), serverAddressPort) # encoding data before sending it to unity

    # Display
    cv2.imshow("Image", img)
    cv2.waitKey(1)