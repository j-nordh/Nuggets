#!/usr/bin/env python
# Log data from serial port


import argparse
import serial
import datetime
import time
import os


parser = argparse.ArgumentParser(formatter_class=argparse.ArgumentDefaultsHelpFormatter)
parser.add_argument("-d", "--device", help="device to read from", default="COM5")
parser.add_argument("-s", "--speed", help="speed in bps", default=9600, type=int)
args = parser.parse_args()

outputFilePath = os.path.join(os.path.dirname(__file__),
                 datetime.datetime.now().strftime("%Y-%m-%d") + ".csv")

print(outputFilePath)

with serial.Serial(args.device, args.speed) as ser, open(outputFilePath,'w') as outputFile:
    print("Logging started. Ctrl-C to stop.") 
    try:
        while True:
            time.sleep(0.2) 
            x = (ser.read(ser.inWaiting())) 
            data = x.decode()
            if data !="":
                outputFile.write(time.strftime("%Y/%m/%d %H:%M ") + " " + data  )
                outputFile.flush()

    except KeyboardInterrupt:
        print("Logging stopped")