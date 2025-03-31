from cvzone.HandTrackingModule import HandDetector # Hand tracking module
import cv2
import socket

# open cv and media pipe are part of the cvzone library you will need to install it and media pipe to run this code

# Parameters
width = 1280
height = 720

# Web Camera
cap = cv2.VideoCapture(0) # Camera Number
cap.set(3, width) # Width 
cap.set(4, height) # Height

# Hand Detector
detector = HandDetector(maxHands =1, detectionCon= 0.8)

# Socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serveraddressport = ("192.168.0.1", 5052)

while True:
    success, img = cap.read() # detecting the camera and continue to read the camera if a camera is present
    
    # Hands
    hands, img = detector.findHands(img)
    
    data = []
    # Transferring landmark data to Unity x,y,z
    if hands:
        hands = hands[0] # get first hand detected
        lmList = hands["lmList"] # List of 21 Landmark points
        # print(lmList) # Display the list of landmark points
        
        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        # print(data)
        sock.sendto(str.encode(str(data)), serveraddressport)
    
    # Display
    cv2.imshow("Webcam", img)
    cv2.waitKey(1)