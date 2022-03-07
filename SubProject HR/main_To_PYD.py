import cv2
import mediapipe as mp
import math
import threading
import time
from threading import Timer

Trace_Coords = []
Coordx = -8787
Coordy = -8787
Coords = []
Init_Coords = []

for i in range(21 * 3):
    Init_Coords.append(-1)

mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles
mp_hands = mp.solutions.hands
image = 0
# For webcam input:
cap = cv2.VideoCapture(0)

def Get_img():
    return image

def Get_87():
    return 87

def Get_Coords():
    return Coords

def Get_Coordx():
    return Coordx

def Get_Coordy():
    return Coordy

def Draw_Trace(_Trace_Coords):
    global image

    for i in range(len(_Trace_Coords)):
        if i < len(_Trace_Coords) - 1:
            _x = _Trace_Coords[i + 1][0] - _Trace_Coords[i][0]
            _y = _Trace_Coords[i + 1][1] - _Trace_Coords[i][1]
            _Vx = _x
            _Vy = _y
            _x = math.fabs(_x)
            _y = math.fabs(_y)
            _Length = math.sqrt(_x * _x + _y * _y)
            if _Length <= 1:
                continue
            _Vx /= _Length
            _Vy /= _Length
            for j in range(int(_Length / 1)):
               cv2.circle(image, (_Trace_Coords[i][0] + int(_Vx * 1 * j), _Trace_Coords[i][1] + int(_Vy * 1 * j)), 5, (255, 255, 0), -1)

def Calculate_Hand_D():
    global Trace_Coords
    global Coords
    global Coordx
    global Coordy
    global mp_drawing
    global mp_drawing_styles
    global mp_hands
    global cap
    global image
    global Init_Coords

    #Coords.clear()
    #Coords.extend(Init_Coords)
    Coords = Init_Coords

    with mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5) as hands:
        success, image = cap.read()
        if not success:
            # Ignoring empty camera frame.
            return

        # Flip the image horizontally for a later selfie-view display, and convert
        # the BGR image to RGB.
        image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
        # To improve performance, optionally mark the image as not writeable to
        # pass by reference.
        image.flags.writeable = False
        results = hands.process(image)

        # Draw the hand annotations on the image.
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        image_height, image_width, _ = image.shape

        if results.multi_hand_landmarks:
          for hand_landmarks in results.multi_hand_landmarks:
            #print("In")
            mp_drawing.draw_landmarks(image, hand_landmarks, mp_hands.HAND_CONNECTIONS, mp_drawing_styles.get_default_hand_landmarks_style(), mp_drawing_styles.get_default_hand_connections_style())
            Trace_Coords.append((int(hand_landmarks.landmark[8].x * image_width), int(hand_landmarks.landmark[8].y * image_height)))
            for i in range(21):
                Coords[i] = hand_landmarks.landmark[i].x
                Coords[i + 21] = hand_landmarks.landmark[i].y
                Coords[i + 21 * 2] = hand_landmarks.landmark[i].z

        if len(Trace_Coords) > 100:
            Trace_Coords = Trace_Coords[len(Trace_Coords) - 100 : len(Trace_Coords)]
        Draw_Trace(Trace_Coords)

        cv2.imshow('MediaPipe Hands', image)
        cv2.waitKey(1)

def Calculate_Hand():
    global Trace_Coords
    global Coords
    global Coordx
    global Coordy
    global mp_drawing
    global mp_drawing_styles
    global mp_hands
    global cap
    global image
    global Init_Coords

    Coords = Init_Coords
    Coordx = -1
    Coordy = -1

    with mp_hands.Hands(min_detection_confidence=0.5, min_tracking_confidence=0.5) as hands:
        success, image = cap.read()
        if not success:
            # Ignoring empty camera frame.
            return

        # Flip the image horizontally for a later selfie-view display, and convert
        # the BGR image to RGB.
        image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
        # To improve performance, optionally mark the image as not writeable to
        # pass by reference.
        image.flags.writeable = False
        results = hands.process(image)

        # Draw the hand annotations on the image.
        image.flags.writeable = True
        image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)
        image_height, image_width, _ = image.shape

        if results.multi_hand_landmarks:
          for hand_landmarks in results.multi_hand_landmarks:
            #mp_drawing.draw_landmarks(image, hand_landmarks, mp_hands.HAND_CONNECTIONS, mp_drawing_styles.get_default_hand_landmarks_style(), mp_drawing_styles.get_default_hand_connections_style())
            Trace_Coords.append((int(hand_landmarks.landmark[8].x * image_width),int(hand_landmarks.landmark[8].y * image_height)))
            '''
            Coordx = hand_landmarks.landmark[8].x
            Coordy = hand_landmarks.landmark[8].y
            '''
            for i in range(21):
                Coords[i] = hand_landmarks.landmark[i].x
                Coords[i + 21] = hand_landmarks.landmark[i].y
                Coords[i + 21 * 2] = hand_landmarks.landmark[i].z
            #print(int(hand_landmarks.landmark[8].x * image_width), ',',int(hand_landmarks.landmark[8].y * image_height))
        '''
        if len(Trace_Coords) > 100:
            Trace_Coords = Trace_Coords[len(Trace_Coords) - 100 : len(Trace_Coords)]
        Draw_Trace(Trace_Coords)

        cv2.imshow('MediaPipe Hands', image)
        cv2.waitKey(1)
        '''

def Calculate_Hand_T():
    while(True):
        Calculate_Hand_D()

def Start_Calculating():
    t = threading.Thread(target = Calculate_Hand_T)
    t.start()
#t = threading.Thread(target = Calculate_Hand_T)
#t.start()
#cap.release()


