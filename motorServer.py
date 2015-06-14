import tornado.ioloop
import tornado.web
import pickle

#import os
#os.system("nohup python servoMotion.py &")
import subprocess
proc = subprocess.Popen(['python', 'servoMotion.py'], shell=False)

class MainHandler(tornado.web.RequestHandler):
    def get(self):
        print dir(self.request)
    def post(self):
        d = self.request.arguments
        xAngle = float( d['xAngle'][0])
        yAngle = float( d['yAngle'][0])
        zAngle = float( d['zAngle'][0])
        #modified x angle from oculus to x angle of motor
        if xAngle>180.0:
            if xAngle>270.0:
                xAngle = xAngle - 270.0
            else:
                xAngle = 0.0
        else:
            if xAngle<90.0:
                xAngle = xAngle + 90.0
            else:
                xAngle = 90.0
        #Modified y angle
        if yAngle>180.0:
            if yAngle>270.0:
                yAngle = yAngle - 270.0
            else:
                yAngle = 0.0
        else:
            if yAngle<90:
                yAngle = yAngle + 90.0
            else:
                yAngle = 90.0
        #Modified z angle
        if zAngle>180.0:
            if zAngle>270.0:
                zAngle = zAngle - 270.0
            else:
                zAngle = 0.0
        else:
            if zAngle<90.0:
                zAngle = zAngle + 90.0
            else:
                zAngle = 90.0
        #time should be sent in microseconds
        perDegree = (2.5 - 0.5)/180.0
        xTime = 0.5 + perDegree * xAngle
        #xTime = 2.5 - perDegree * xAngle
        yTime = 0.5 + perDegree * yAngle
        #zTime = 0.5 + perDegree * zAngle
        zTime = 2.5 - perDegree * zAngle
        angles = {'xTime': xTime, 'yTime': yTime, 'zTime': zTime }
        pickle.dump(angles, open("angles.p", "wb"))

application = tornado.web.Application([
    (r"/", MainHandler),
])

if __name__ == "__main__":
    try:
        application.listen(8888)
        tornado.ioloop.IOLoop.instance().start()
    except KeyboardInterrupt:
        proc.terminate()


