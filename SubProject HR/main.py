import threading
import cv2
import sys
sys.path.append("V:\Work\Projects\Project CS\SubProject HR")

#import main_To_PYD
import HR_PYD

def Run():
    while 1 == 1:
        HR_PYD.Calculate_Hand()

#HR_PYD.Calculate_Hand_T()
#HR_PYD.Start_Calculating()
while 1==1:
    #HR_PYD.Calculate_Hand()
    #cv2.imshow('MediaPipe Hands', HR_PYD.Get_img())
    #if(len(HR_PYD.Get_Coords()) >= 21*3):
    HR_PYD.Calculate_Hand_D()
    print(HR_PYD.Get_Coords()[0], HR_PYD.Get_Coords()[21 + 0])
    '''
    for _item in HR_PYD.Get_Testx():
        print(_item)
    '''

