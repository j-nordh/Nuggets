import serial
from datetime import datetime

dateStr = datetime.now().isoformat()[0:10]

fileName = f"C:\\Users\\MTkFotRad\\Documents\\logFile_{dateStr}.csv"

dataFile = open(fileName, 'a')

ser = serial.Serial('COM4', 115200)

count = 0

while True:
    try:
        data = ser.readline().rstrip().decode("UTF-8")
        data = str(datetime.now()) + ";" + data + "\n"
        print(data)
        dataFile.write(data)

        count+=1
        if(count > 100):
            count=0
            dataFile.flush()
    except KeyboardInterrupt:
        break

dataFile.close()
ser.close()

    