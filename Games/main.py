import cv2
import mediapipe as mp
import win32pipe, win32file

class PipeServer():
    def __init__(self, pipeName):
        self.pipe = win32pipe.CreateNamedPipe(
            r'\\.\pipe\\' + pipeName,
            win32pipe.PIPE_ACCESS_OUTBOUND,
            win32pipe.PIPE_TYPE_MESSAGE | win32pipe.PIPE_READMODE_MESSAGE | win32pipe.PIPE_WAIT,
            1, 65536, 65536,
            0,
            None)
    def connect(self):
        win32pipe.ConnectNamedPipe(self.pipe, None)
    def write(self, message):
        win32file.WriteFile(self.pipe, message.encode() + b'\n')
    def close(self):
        win32file.CloseHandle(self.pipe)

class handTracker():
    def __init__(self, mode=False, maxHands=1, detectionCon=0.5,modelComplexity=1,trackCon=0.5):
        self.mode = mode
        self.maxHands = maxHands
        self.detectionCon = detectionCon
        self.modelComplex = modelComplexity
        self.trackCon = trackCon
        self.mpHands = mp.solutions.hands
        self.hands = self.mpHands.Hands(self.mode, self.maxHands,self.modelComplex,
                                        self.detectionCon, self.trackCon)
        self.mpDraw = mp.solutions.drawing_utils
    def handsFinder(self,image,draw=True):
        imageRGB = cv2.cvtColor(image,cv2.COLOR_BGR2RGB)
        self.results = self.hands.process(imageRGB)

        if self.results.multi_hand_landmarks:
            for handLms in self.results.multi_hand_landmarks:
                if draw:
                    self.mpDraw.draw_landmarks(image, handLms, self.mpHands.HAND_CONNECTIONS)
        return image

    def positionFinder(self, image, handNo=0, draw=True):
        lmlist = []
        if self.results.multi_hand_landmarks:
            Hand = self.results.multi_hand_landmarks[handNo]
            for id, lm in enumerate(Hand.landmark):
                h, w, c = image.shape
                cx, cy = int(lm.x * w), int(lm.y * h)
                lmlist.append([id, cx, cy])
                if draw and id==9:
                    cv2.circle(image, (0, cy), 5, (255, 0, 255), cv2.FILLED)
        return lmlist

def main():
    t = PipeServer("CSServer")
    t.connect()
    cap = cv2.VideoCapture(0)
    if not  cap.isOpened() :
        t.write("VideoClosed")

    tracker = handTracker()
    cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1920)
    cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 1080)
    
    t.write("ready")
    while True:
        success, image = cap.read()
        image = tracker.handsFinder(image)
        lmList = tracker.positionFinder(image)
        if len(lmList) != 0:
            if lmList[9][2] < 0:
                cord=0
            else :
                cord =lmList[9][2]
            cordFinal=cord
            t.write(str(cordFinal))

if __name__ == "__main__":
    main()
