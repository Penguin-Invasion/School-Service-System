import requests
import cv2
import pytesseract
import imutils
import numpy as np

#Starting Optical Character Recognition Software
pytesseract.pytesseract.tesseract_cmd = r'C:\Program Files\Tesseract-OCR\tesseract.exe'

def readimage(fileName):
    img = cv2.imread(fileName) #Read the image
    img = cv2.resize(img, (620,480) ) #Resize the image to a proper size
    return img

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
    screenCnt = np.empty( shape=(0, 0) )
    for c in contours:
        peri = cv2.arcLength(c, True) # approximate the contour
        approx = cv2.approxPolyDP(c, 0.015 * peri, True)
        if len(approx) == 4: # if our approximated contour has four points, then it's a rectangle which is probably the plate
            screenCnt = approx
            break
    return screenCnt

def cropPlate(screenCnt,gray,img):
    mask = np.zeros(gray.shape,np.uint8)
    new_image = cv2.drawContours(mask,[screenCnt],0,255,-1,)
    new_image = cv2.bitwise_and(img,img,mask=mask)
    (x, y) = np.where(mask == 255)
    (topx, topy) = (np.min(x), np.min(y))
    (bottomx, bottomy) = (np.max(x), np.max(y))
    cropped = gray[topx:bottomx+1, topy:bottomy+1]
    #threshold,thresh = cv2.threshold(cropped,75,255,cv2.THRESH_BINARY)
    #cv2.imshow("sa",thresh)
    return cropped

def applyOcr(img) : return pytesseract.image_to_string(img, config='--psm 11').replace("\n","")

extension = ".jpg"
names = ["1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22"]
directory = "images/"
for i in range(22):
    fileName = directory + names[i] + extension
    img=readimage(fileName)
    gray = applyFilters(img)
    contours = detectContours(gray)
    screenCnt = detectPlate(contours)
    if(screenCnt.size == 0) : print("There is no plate")
    else:
        plateImg = cropPlate(screenCnt,gray,img)
        plate = applyOcr(plateImg)
        if(len(plate) > 0 ) : print(plate)
        else : print("Read nothing from cropped image")
    

#url = "http://192.168.1.24:8080/shot.jpg"
#while True:
#    img_resp = requests.get(url)
#    img_arr = np.array(bytearray(img_resp.content), dtype=np.uint8)
 #   img = cv2.imdecode(img_arr, -1)
    #  img = imutils.resize(img, width=620, height=480)
    # cv2.imshow("Android_cam", img)
    #if cv2.waitKey(1) == 27:
    #    break
    # else:
    #    gray = applyFilters(img)
    #    contours = detectContours(gray)
    #  screenCnt = detectPlate(contours)
    #  if(screenCnt.size == 0) : print("There is no plate")
    #  else:
    #     plateImg = cropPlate(screenCnt,gray,img)
    #    plate = applyOcr(plateImg)
    #    if(len(plate) > 0 ) : print(plate)
#   else : print("Read nothing from cropped image")
  
#cv2.destroyAllWindows()