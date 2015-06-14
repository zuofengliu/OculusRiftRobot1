import RPi.GPIO as GPIO
import time
import pickle

pinX = 10
pinY = 12
pinZ = 13
GPIO.setmode(GPIO.BOARD)
GPIO.setup(pinX, GPIO.OUT)
GPIO.setup(pinY, GPIO.OUT)
GPIO.setup(pinZ, GPIO.OUT)

i = 5
xTime = 1.5/1000
yTime = 1.5/1000
zTime = 1.5/1000
cycleTime = 20.0/1000 - xTime -yTime - zTime

while True:
    if i==5:
        try:
            angles = pickle.load( open('angles.p', 'rb'))
            xTime = angles['xTime']/1000
            yTime = angles['yTime']/1000
            zTime = angles['zTime']/1000
            cycleTime = 20.0/1000 - xTime -yTime - zTime
            i=1
        except:
            i = i-1

    GPIO.output(pinX, True)
    time.sleep(xTime)
    GPIO.output(pinX, False)

    GPIO.output(pinY, True)
    time.sleep(yTime)
    GPIO.output(pinY, False)

    GPIO.output(pinZ, True)
    time.sleep(zTime)
    GPIO.output(pinZ, False)

    time.sleep(cycleTime)
    i = i + 1

if __name__=='__main__':
    pass

