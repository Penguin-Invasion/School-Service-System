from numpy.lib.npyio import recfromtxt
import requests
import cv2
import pytesseract
import imutils
import numpy as np
from tkinter import *
from PIL import Image,ImageTk

#Starting Optical Character Recognition Software
pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files (x86)\Tesseract-OCR\tesseract.exe'

def applyFilters(img):
    gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY) #Convert to gray scale
    gray = cv2.bilateralFilter(gray, 13, 30, 30) #Apply blur 25 was good
    return gray

def detectContours(img):
    img = cv2.Canny(img, 30, 200) #Apply Canny Edge Detection
    contours=cv2.findContours(img.copy(),cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)
    contours = imutils.grab_contours(contours)
    contours = sorted(contours,key=cv2.contourArea, reverse = True)[:10]
    return contours

def detectPlate(contours):
    global recx
    global recy 
    global recw 
    global rech
    screenCnt = np.empty( shape=(0, 0) )
    for c in contours:
        peri = cv2.arcLength(c, True) # approximate the contour
        approx = cv2.approxPolyDP(c, 0.015 * peri, True)
        if len(approx) == 4: # if our approximated contour has four points, then it's a rectangle which is probably the plate
            screenCnt = approx
            recx,recy,recw,rech = cv2.boundingRect(approx)
            break
    return screenCnt

def cropPlate(screenCnt,gray,img):
    global topx
    global topy
    global bottomx
    global bottomy
    mask = np.zeros(gray.shape,np.uint8)
    new_image = cv2.drawContours(mask,[screenCnt],0,255,-1,)
    new_image = cv2.bitwise_and(img,img,mask=mask)
    (x, y) = np.where(mask == 255)
    (topx, topy) = (np.min(x), np.min(y))
    (bottomx, bottomy) = (np.max(x), np.max(y))
    cropped = gray[topx:bottomx+1, topy:bottomy+1]
    #threshold,thresh = cv2.threshold(cropped,75,255,cv2.THRESH_BINARY)
    return cropped

def applyOcr(img) : return pytesseract.image_to_string(img, config='--psm 11').replace("\n","")

recx,recy,recw,rech = 0,0,0,0
topx,topy,bottomx,bottomy = 0,0,0,0
window = Tk() #instantiate an instance of a window
window.geometry("1280x600")
window.title("School Service System")
window.config(background="#000000")
icon = PhotoImage(file='icon.png')
label = Label(window,image=icon,bg = "#000000",borderwidth=0,highlightthickness=0)
textCompany = Label(window,text= "School Service System",font=('Arial',35),fg="#FFFFFF",bg = "#000000",borderwidth=0,highlightthickness=0)
label.place(x=50,y=50)
textCompany.place(x=40,y=375)
window.iconphoto(True,icon)
url = "http://192.168.1.24:8080/shot.jpg"
textPlate = Label(window)
while True:
    img_resp = requests.get(url)
    img_arr = np.array(bytearray(img_resp.content), dtype=np.uint8)
    img = cv2.imdecode(img_arr, -1)
    img = imutils.resize(img, width=620, height=480)
    if cv2.waitKey(1) == 27:
        break
    else:
        gray = applyFilters(img)
        contours = detectContours(gray)
        screenCnt = detectPlate(contours)
        if(screenCnt.size == 0) : print("There is no plate")
        else:
            plateImg = cropPlate(screenCnt,gray,img)
            plate = applyOcr(plateImg)
            if(len(plate) > 0 ):
                msg = "Detected plate: " + plate
                textPlate = Label(window,text= msg,font=('Arial',30),fg="#FFFFFF",bg = "#000000",borderwidth=0,highlightthickness=0)
                textPlate.place(x=600,y=450)
                cv2.rectangle(img,(recx,recy),(recx + recw,recy + rech),(0,255,0),2)
                pass
            else:
                print("Read nothing from cropped image")
        b,g,r = cv2.split(img)
        img = cv2.merge((r,g,b))
        im = Image.fromarray(img)
        guimage = ImageTk.PhotoImage(image=im) 
        imglabel = Label(window,image=guimage,bg = "#000000",borderwidth=0,highlightthickness=0)
        imglabel.place(x=600,y=50)
        window.update()
        textPlate.place(x=-500,y=-500)
        
  
cv2.destroyAllWindows()