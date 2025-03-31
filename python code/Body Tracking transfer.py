import cv2
from cvzone.PoseModule import PoseDetector
import socket

# Initialize webcam
cap = cv2.VideoCapture(0)
cap.set(3, 1280)  # Width
cap.set(4, 720)   # Height

# Initialize Pose Detector
detector = PoseDetector()

# Initialize socket for sending data
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5052)

while True:
    success, img = cap.read()
    if not success:
        break

    # Find the pose and draw landmarks
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img, bboxWithHands=True)

    if lmList:
        # Extract coordinates of landmarks 12, 11, 24, and 23
        points = [lmList[12], lmList[11], lmList[24], lmList[23]]

        # Calculate the middle point
        x_avg = sum(point[0] for point in points) / len(points)
        y_avg = sum(point[1] for point in points) / len(points)
        middle_point = (int(x_avg), int(y_avg))

        # Draw the middle point on the image
        cv2.circle(img, middle_point, 5, (0, 255, 0), cv2.FILLED)
        print(f"Middle point: {middle_point}")

        # Prepare data to send
        data = [middle_point[0], middle_point[1]]
        for point in points:
            data.extend([point[0], point[1]])

        # Send data to the server
        sock.sendto(str.encode(str(data)), serverAddressPort)

    # Display the image
    cv2.imshow("Image", img)
    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()