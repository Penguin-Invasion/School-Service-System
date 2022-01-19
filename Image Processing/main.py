import requests
import cv2
import pytesseract
import imutils
import numpy as np
import re
import pystray
from pystray import MenuItem as item
from tkinter import *
from PIL import Image,ImageTk
import time

#Starting Optical Character Recognition Software
pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files (x86)\Tesseract-OCR\tesseract.exe'

def applyFilters(img):
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY) #Convert to gray scale
    return cv2.bilateralFilter(gray, 13, 30, 30)

def detectContours(img):
    img = cv2.Canny(img, 30, 200) #Apply Canny Edge Detection
    contours=cv2.findContours(img.copy(),cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    contours = imutils.grab_contours(contours)
    return sorted(contours,key=cv2.contourArea, reverse = True)[:10]

def detectPlate(contours):
    screenCnt = np.empty( shape=(0, 0) )
    for c in contours:
        peri = cv2.arcLength(c, True) # approximate the contour
        approx = cv2.approxPolyDP(c, 0.015 * peri, True)
        if len(approx) == 4: # if our approximated contour has four points, then it's a rectangle which is probably the plate
            screenCnt = approx
            recx,recy,recw,rech = cv2.boundingRect(approx)
            Rect[0],Rect[1],Rect[2],Rect[3] = recx,recy,recw,rech
            break
    return screenCnt

def cropPlate(screenCnt,gray,img):
    mask = np.zeros(gray.shape,np.uint8)
    new_image = cv2.drawContours(mask,[screenCnt],0,255,-1,)
    new_image = cv2.bitwise_and(img,img,mask=mask)
    (x, y) = np.where(mask == 255)
    (topx, topy) = (np.min(x), np.min(y))
    (bottomx, bottomy) = (np.max(x), np.max(y))
    #Binary thresholding may be useful
    return gray[topx:bottomx+1, topy:bottomy+1]

def applyOcr(img) : return pytesseract.image_to_string(img, config='--psm 11').replace("\n","")

def createWindow():
    window.geometry("1280x600")
    window.title("School Service System")
    window.config(background="#000000")
    label = Label(window,image=icon,bg = "#000000",borderwidth=0,highlightthickness=0)
    textCompany = Label(window,text= "School Service System",font=('Arial',35),fg="#FFFFFF",bg = "#000000",borderwidth=0,highlightthickness=0)
    label.place(x=50,y=50)
    textCompany.place(x=40,y=375)
    window.iconphoto(True,icon)

def getImageFromCamera():
    img_resp = requests.get(url)
    img_arr = np.array(bytearray(img_resp.content), dtype=np.uint8)
    img = cv2.imdecode(img_arr, -1)
    return imutils.resize(img, width=620, height=480)

def showPlateInfo(plate):
    global textPlate
    msg = "Detected plate: " + plate
    textPlate = Label(window,text= msg,font=('Arial',30),fg="#FFFFFF",bg = "#000000",borderwidth=0,highlightthickness=0)
    textPlate.place(x=600,y=450)

def convertImage(img):
    b,g,r = cv2.split(img)
    img = cv2.merge((r,g,b))
    return Image.fromarray(img)

def removeInvalidCharacters(str):
    rtr = ""
    for i in str: 
        if(i.isalnum()) : rtr += i
    return rtr

def isPlate(str):
    regex = "([0-9]{2})([A-Z]{1,3})([0-9]{2,4})"
    if(re.search(regex,str) == None):  return FALSE
    return TRUE
    
# Define a function for quit the window
def quit_window(icon, item):
   icon.stop()
   window.destroy()

# Define a function to show the window again
def show_window(icon, item):
   icon.stop()
   window.deiconify()


def closeButton():
    window.withdraw()
    image=Image.open("image.ico")
    menu = pystray.Menu(item('Show', show_window, default=True),item('Quit', quit_window))
    icon=pystray.Icon("School Service System", image, "School Service System", menu)
    icon.run()

    

Rect = [0,0,0,0]
gui = TRUE
window = Tk() #instantiate an instance of a window
window.protocol("WM_DELETE_WINDOW", closeButton)
plates = []
times = []
plateList = [times,plates]
icon = PhotoImage(file='icon.png')
textPlate = Label(window)
window.overrideredirect()
createWindow()
with open("IpCameraUrl.txt","r") as file : url = file.read()
while True:
    img = getImageFromCamera()
    gray = applyFilters(img)
    contours = detectContours(gray)
    screenCnt = detectPlate(contours)
    if(screenCnt.size > 0):
        plateImg = cropPlate(screenCnt,gray,img)
        scannedText = removeInvalidCharacters(applyOcr(plateImg))
        if(isPlate(scannedText)): 
            showPlateInfo(scannedText)
            cv2.rectangle(img,(Rect[0],Rect[1]),(Rect[0] + Rect[2],Rect[1] + Rect[3]),(0,255,0),2)
            if(scannedText in plateList[1]):
                for i in range(len(plateList[1])):
                    if(plateList[1][i] == scannedText):
                        if( time.time() - plateList[0][i] > 60): 
                            plateList[0][i] = time.time()
                            requests.post("https://schoolservicesystem.azurewebsites.net/api/Entry?SecretKey=RKF6GUMC84WH2BRM7FRLOK67WWRSZG&Plaque=" + scannedText)
                            break
            else:
                plateList[0].append(time.time())
                plateList[1].append(scannedText)
                requests.post("https://schoolservicesystem.azurewebsites.net/api/Entry?SecretKey=RKF6GUMC84WH2BRM7FRLOK67WWRSZG&Plaque=" + scannedText)
    guimage = ImageTk.PhotoImage(image=convertImage(img)) 
    Label(window,image=guimage,bg = "#000000",borderwidth=0,highlightthickness=0).place(x=600,y=50)
    window.update()
    textPlate.place(x=-500,y=-500)