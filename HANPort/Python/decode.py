# -*- coding: utf-8 -*-
"""
Created on Mon Jun 27 12:47:17 2022

@author: niklaser2
"""

import re
import datetime


def timestamp(v):
    return (datetime.datetime.strptime(v[:-1], "%y%m%d%H%M%S").isoformat(), v[-1])

def start(v):
    return v

def end(v):
    return v


reg_v = [['^\/(.*)', 'id', start],
         ['^!(.*)', 'crc', end],
         ['^0-0:1\.0\.0\((\d+.)\)', 'timestamp', timestamp],
         ['^1-0:1\.8\.0\((\d+\.\d+)\*kWh\)', 'Active_E_exp', float],
         ['^1-0:2\.8\.0\((\d+\.\d+)\*kWh\)', 'Active_E_imp', float],
         ['^1-0:3\.8\.0\((\d+\.\d+)\*kVArh\)', 'Reactive_E_exp', float],
         ['^1-0:4\.8\.0\((\d+\.\d+)\*kVArh\)', 'Reactive_E_imp', float],
         ['^1-0:1\.7\.0\((\d+\.\d+)\*kW\)', 'Active_P_exp', float],
         ['^1-0:2\.7\.0\((\d+\.\d+)\*kW\)', 'Active_P_imp', float],
         ['^1-0:3\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_Q_exp', float],
         ['^1-0:4\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_Q_imp', float],
         ['^1-0:21\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL1_exp', float],
         ['^1-0:22\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL1_imp', float],
         ['^1-0:41\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL2_exp', float],
         ['^1-0:42\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL2_imp', float],
         ['^1-0:61\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL3_exp', float],
         ['^1-0:62\.7\.0\((\d+\.\d+)\*kW\)', 'Active_PL3_imp', float],
         ['^1-0:23\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL1_exp', float],
         ['^1-0:24\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL1_imp', float],
         ['^1-0:43\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL2_exp', float],
         ['^1-0:44\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL2_imp', float],
         ['^1-0:63\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL3_exp', float],
         ['^1-0:64\.7\.0\((\d+\.\d+)\*kVAr\)', 'Reactive_QL3_imp', float],
         ['^1-0:32\.7\.0\((\d+\.\d+)\*V\)', 'Phase_UL1', float],
         ['^1-0:52\.7\.0\((\d+\.\d+)\*V\)', 'Phase_UL2', float],
         ['^1-0:72\.7\.0\((\d+\.\d+)\*V\)', 'Phase_UL3', float],
         ['^1-0:31\.7\.0\((\d+\.\d+)\*A\)', 'Phase_IL1', float],
         ['^1-0:51\.7\.0\((\d+\.\d+)\*A\)', 'Phase_IL2', float],
         ['^1-0:71\.7\.0\((\d+\.\d+)\*A\)', 'Phase_IL3', float],
         ]

def decode(data):
    d_v = {}
    for line in data.splitlines():
        found = False
        for reg, lbl, fcn in reg_v:
            m = re.search(reg, line)
            if m is not None:
                v = m.group(1)
                d_v[lbl] = fcn(v)
                found = True
                break
        if not found and len(line.strip()) > 0:
            print(f'Not found: <{line}>')
    return d_v



if __name__ == "__main__":
    filename = r"C:\\Users\\alexanderkl\\RISE\\Mattias Persson - HANporten\\kod\\py\\telegram.txt"

    with open(filename, "r") as f:
        data = f.read()

    d_v = decode(data)
    print(d_v)

    print(data)