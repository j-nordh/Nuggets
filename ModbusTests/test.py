#from pymodbus.client.sync import ModbusTcpClient
import pymodbus
from pymodbus.client.sync import ModbusTcpClient
from struct import *

# import logging
# FORMAT = ('%(asctime)-15s %(threadName)-15s '
#           '%(levelname)-8s %(module)-15s:%(lineno)-8s %(message)s')
# logging.basicConfig(format=FORMAT)
# log = logging.getLogger()
# log.setLevel(logging.DEBUG)

#UNIT = 0x1
#UNIT = 0x4
UNIT = 0x3

#client = ModbusTcpClient('192.168.1.120')
#client = ModbusTcpClient('192.168.1.67')

client = ModbusTcpClient('192.168.2.11')

#ModbusTcpClient
#304353
res = client.read_holding_registers(address=1000,count=1,unit=UNIT)
#res = client.read_holding_registers(address=0x005E,count=2,unit=UNIT)
#res = client.read_input_registers(1000, unit=UNIT)


###res = client.write_register(address=0x, value=0x0002, unit=UNIT)


#res = client.read_holding_registers(0x1001, count=1, unit=UNIT)

#res = client.write_register(0x1101, unit=UNIT, value=0x7)

#res = client.write_coil(0x1101, unit=UNIT, value=0x7)

#res = client.write_coils(0x1101, unit=UNIT, count=1, values=[0x7])

#res = client.write_registers(0x1101, unit=UNIT, count=1, values=[0x7])




#res = client.read_input_registers(0x0000)

print(res)
print(res.registers)

# for num in res.registers:
#     b = pack('<H', num)
#     print(b)
#     str_bye = str(b)
#     #test = str_bye.encode('ascii',errors='ignore').decode()
#     #print(test)


client.close()