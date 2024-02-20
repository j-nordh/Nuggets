EESchema Schematic File Version 4
EELAYER 30 0
EELAYER END
$Descr A4 11693 8268
encoding utf-8
Sheet 1 1
Title "MTDevBoard"
Date "2021-04-29"
Rev "2"
Comp "RISE"
Comment1 "Johannes Nordh"
Comment2 "Mätteknik"
Comment3 ""
Comment4 ""
$EndDescr
$Comp
L Converter_ACDC:IRM-20-5 Power1
U 1 1 5FE906B8
P 10000 1150
F 0 "Power1" H 10000 1475 50  0000 C CNN
F 1 "IRM-20-5" H 10000 1384 50  0000 C CNN
F 2 "Converter_ACDC:Converter_ACDC_MeanWell_IRM-20-xx_THT" H 10000 850 50  0001 C CNN
F 3 "http://www.meanwell.com/Upload/PDF/IRM-20/IRM-20-SPEC.PDF" H 10400 800 50  0001 C CNN
	1    10000 1150
	-1   0    0    -1  
$EndComp
$Comp
L power:GND #PWR018
U 1 1 5FE91602
P 9500 1200
F 0 "#PWR018" H 9500 950 50  0001 C CNN
F 1 "GND" H 9505 1027 50  0000 C CNN
F 2 "" H 9500 1200 50  0001 C CNN
F 3 "" H 9500 1200 50  0001 C CNN
	1    9500 1200
	-1   0    0    -1  
$EndComp
$Comp
L ESP32-DevKit:ESP32-DevKit-Lipo U1
U 1 1 5FE91FF0
P 2475 1800
F 0 "U1" H 2475 3065 50  0000 C CNN
F 1 "ESP32-DevKit-Lipo" H 2475 2974 50  0000 C CNN
F 2 "ESP32-DevKit-Lipo:DIP-38_1000_ELL" H 2475 1800 50  0001 C CNN
F 3 "DOCUMENTATION" H 2475 1800 50  0001 C CNN
	1    2475 1800
	1    0    0    -1  
$EndComp
$Comp
L power:GND #PWR02
U 1 1 5FE95D1C
P 3225 900
F 0 "#PWR02" H 3225 650 50  0001 C CNN
F 1 "GND" V 3230 772 50  0000 R CNN
F 2 "" H 3225 900 50  0001 C CNN
F 3 "" H 3225 900 50  0001 C CNN
	1    3225 900 
	0    -1   -1   0   
$EndComp
$Comp
L power:GND #PWR03
U 1 1 5FE9665F
P 3225 1500
F 0 "#PWR03" H 3225 1250 50  0001 C CNN
F 1 "GND" V 3230 1372 50  0000 R CNN
F 2 "" H 3225 1500 50  0001 C CNN
F 3 "" H 3225 1500 50  0001 C CNN
	1    3225 1500
	0    -1   -1   0   
$EndComp
$Comp
L power:GND #PWR01
U 1 1 5FE96A57
P 1725 2200
F 0 "#PWR01" H 1725 1950 50  0001 C CNN
F 1 "GND" V 1730 2072 50  0000 R CNN
F 2 "" H 1725 2200 50  0001 C CNN
F 3 "" H 1725 2200 50  0001 C CNN
	1    1725 2200
	0    1    1    0   
$EndComp
Text GLabel 9500 1100 1    50   Input ~ 0
5V
Text GLabel 1725 2700 0    50   Input ~ 0
5V
$Comp
L Connector:Screw_Terminal_01x02 J14
U 1 1 5FE97AC4
P 10950 1250
F 0 "J14" H 10868 1017 50  0000 C CNN
F 1 "AC In" V 10823 1330 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 10950 1250 50  0001 C CNN
F 3 "~" H 10950 1250 50  0001 C CNN
	1    10950 1250
	1    0    0    1   
$EndComp
Wire Wire Line
	10500 1050 10400 1050
$Comp
L Device:Q_NPN_EBC Q1
U 1 1 5FE9B10C
P 9600 2800
F 0 "Q1" H 9791 2846 50  0000 L CNN
F 1 "Q_Relay1" H 9200 2650 50  0000 L CNN
F 2 "Package_TO_SOT_THT:TO-92_Inline" H 9800 2900 50  0001 C CNN
F 3 "~" H 9600 2800 50  0001 C CNN
	1    9600 2800
	-1   0    0    -1  
$EndComp
$Comp
L power:GND #PWR016
U 1 1 5FEA0642
P 9500 3000
F 0 "#PWR016" H 9500 2750 50  0001 C CNN
F 1 "GND" V 9505 2827 50  0001 C CNN
F 2 "" H 9500 3000 50  0001 C CNN
F 3 "" H 9500 3000 50  0001 C CNN
	1    9500 3000
	1    0    0    -1  
$EndComp
Wire Wire Line
	9500 2100 9850 2100
Wire Wire Line
	9500 2250 9500 2100
Text GLabel 9500 2100 0    50   Input ~ 0
5V
$Comp
L Device:D D1
U 1 1 5FE9BF64
P 9500 2400
F 0 "D1" V 9454 2479 50  0000 L CNN
F 1 "D" V 9545 2479 50  0000 L CNN
F 2 "Diode_THT:D_A-405_P7.62mm_Horizontal" H 9500 2400 50  0001 C CNN
F 3 "~" H 9500 2400 50  0001 C CNN
	1    9500 2400
	0    1    1    0   
$EndComp
$Comp
L Relay:SANYOU_SRD_Form_C K1
U 1 1 5FE9964C
P 10050 2400
F 0 "K1" H 10475 2575 50  0000 L CNN
F 1 "Relay1" H 10480 2355 50  0001 L CNN
F 2 "Relay_THT:Relay_SPDT_SANYOU_SRD_Series_Form_C" H 10500 2350 50  0001 L CNN
F 3 "http://www.sanyourelay.ca/public/products/pdf/SRD.pdf" H 10050 2400 50  0001 C CNN
	1    10050 2400
	1    0    0    -1  
$EndComp
$Comp
L Device:R R5
U 1 1 5FE9D0D5
P 9950 2800
F 0 "R5" V 9950 2800 50  0000 C CNN
F 1 "R" V 9834 2800 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 9880 2800 50  0001 C CNN
F 3 "~" H 9950 2800 50  0001 C CNN
	1    9950 2800
	0    1    1    0   
$EndComp
Wire Wire Line
	10250 2700 10500 2700
Wire Wire Line
	10350 2100 10600 2100
Wire Wire Line
	10600 2100 10600 2500
Wire Wire Line
	10150 2100 10150 2050
Wire Wire Line
	10500 2700 10500 2400
Wire Wire Line
	10600 2500 10700 2500
Wire Wire Line
	10500 2400 10700 2400
Wire Wire Line
	10150 2050 10700 2050
Wire Wire Line
	10700 2050 10700 2300
$Comp
L Device:Q_NPN_EBC Q2
U 1 1 5FECC731
P 9600 3900
F 0 "Q2" H 9791 3946 50  0000 L CNN
F 1 "Q_Relay2" H 9200 3750 50  0000 L CNN
F 2 "Package_TO_SOT_THT:TO-92_Inline" H 9800 4000 50  0001 C CNN
F 3 "~" H 9600 3900 50  0001 C CNN
	1    9600 3900
	-1   0    0    -1  
$EndComp
$Comp
L power:GND #PWR017
U 1 1 5FECC737
P 9500 4100
F 0 "#PWR017" H 9500 3850 50  0001 C CNN
F 1 "GND" H 9650 4050 50  0001 C CNN
F 2 "" H 9500 4100 50  0001 C CNN
F 3 "" H 9500 4100 50  0001 C CNN
	1    9500 4100
	1    0    0    -1  
$EndComp
Wire Wire Line
	9500 3200 9850 3200
Wire Wire Line
	9500 3350 9500 3200
Text GLabel 9500 3200 0    50   Input ~ 0
5V
$Comp
L Device:D D2
U 1 1 5FECC742
P 9500 3500
F 0 "D2" V 9454 3579 50  0000 L CNN
F 1 "D" V 9545 3579 50  0000 L CNN
F 2 "Diode_THT:D_A-405_P7.62mm_Horizontal" H 9500 3500 50  0001 C CNN
F 3 "~" H 9500 3500 50  0001 C CNN
	1    9500 3500
	0    1    1    0   
$EndComp
$Comp
L Relay:SANYOU_SRD_Form_C K2
U 1 1 5FECC748
P 10050 3500
F 0 "K2" H 10480 3500 50  0000 L CNN
F 1 "Relay2" H 10480 3455 50  0001 L CNN
F 2 "Relay_THT:Relay_SPDT_SANYOU_SRD_Series_Form_C" H 10500 3450 50  0001 L CNN
F 3 "http://www.sanyourelay.ca/public/products/pdf/SRD.pdf" H 10050 3500 50  0001 C CNN
	1    10050 3500
	1    0    0    -1  
$EndComp
$Comp
L Device:R R6
U 1 1 5FECC74E
P 9950 3900
F 0 "R6" V 9950 3900 50  0000 C CNN
F 1 "R" V 9834 3900 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 9880 3900 50  0001 C CNN
F 3 "~" H 9950 3900 50  0001 C CNN
	1    9950 3900
	0    1    1    0   
$EndComp
Wire Wire Line
	10250 3800 10650 3800
Wire Wire Line
	10350 3200 10600 3200
Wire Wire Line
	10600 3200 10600 3600
Wire Wire Line
	10150 3200 10150 3150
Text GLabel 10100 3900 2    50   Input ~ 0
Relay2
Wire Wire Line
	10650 3800 10650 3500
Wire Wire Line
	10600 3600 10700 3600
Wire Wire Line
	10650 3500 10700 3500
Wire Wire Line
	10150 3150 10700 3150
Wire Wire Line
	10700 3150 10700 3400
$Comp
L Connector:Screw_Terminal_01x02 J7
U 1 1 5FECCB54
P 8450 1000
F 0 "J7" V 8368 1080 50  0000 L CNN
F 1 "5V" V 8323 1080 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 8450 1000 50  0001 C CNN
F 3 "~" H 8450 1000 50  0001 C CNN
	1    8450 1000
	0    1    -1   0   
$EndComp
Text GLabel 8350 1200 3    50   Input ~ 0
5V
$Comp
L power:GND #PWR011
U 1 1 5FECD8B7
P 8450 1200
F 0 "#PWR011" H 8450 950 50  0001 C CNN
F 1 "GND" H 8455 1027 50  0000 C CNN
F 2 "" H 8450 1200 50  0001 C CNN
F 3 "" H 8450 1200 50  0001 C CNN
	1    8450 1200
	1    0    0    -1  
$EndComp
Text GLabel 1725 900  0    50   Input ~ 0
3V3
$Comp
L Connector:Screw_Terminal_01x02 J9
U 1 1 5FED09BB
P 8875 1000
F 0 "J9" V 8793 1080 50  0000 L CNN
F 1 "3V3" V 8748 1080 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 8875 1000 50  0001 C CNN
F 3 "~" H 8875 1000 50  0001 C CNN
	1    8875 1000
	0    1    -1   0   
$EndComp
$Comp
L power:GND #PWR015
U 1 1 5FED09C2
P 8875 1200
F 0 "#PWR015" H 8875 950 50  0001 C CNN
F 1 "GND" H 8880 1027 50  0000 C CNN
F 2 "" H 8875 1200 50  0001 C CNN
F 3 "" H 8875 1200 50  0001 C CNN
	1    8875 1200
	1    0    0    -1  
$EndComp
Text GLabel 8775 1200 3    50   Input ~ 0
3V3
$Comp
L Connector:UEXT_Host J3
U 1 1 5FED0EED
P 5525 2350
F 0 "J3" H 5225 2750 50  0000 C CNN
F 1 "UEXT_Host" H 5775 2750 50  0000 C CNN
F 2 "Connector_IDC:IDC-Header_2x05_P2.54mm_Vertical" H 5375 2350 50  0001 C CNN
F 3 "https://www.olimex.com/Products/Modules/UEXT/resources/UEXT_rev_B.pdf" H 5375 2350 50  0001 C CNN
	1    5525 2350
	1    0    0    -1  
$EndComp
Text GLabel 5525 1850 0    50   Input ~ 0
3V3
Text GLabel 3225 1600 2    50   Input ~ 0
19
Text GLabel 3225 1700 2    50   Input ~ 0
18
Text GLabel 3225 1800 2    50   Input ~ 0
05
$Comp
L power:GND #PWR06
U 1 1 5FED7A2E
P 5525 2850
F 0 "#PWR06" H 5525 2600 50  0001 C CNN
F 1 "GND" H 5530 2677 50  0001 C CNN
F 2 "" H 5525 2850 50  0001 C CNN
F 3 "" H 5525 2850 50  0001 C CNN
	1    5525 2850
	1    0    0    -1  
$EndComp
Wire Wire Line
	10400 1250 10750 1250
Wire Wire Line
	10500 1050 10500 1150
Wire Wire Line
	10500 1150 10750 1150
Text GLabel 3225 1400 2    50   Input ~ 0
21
Text GLabel 3225 1900 2    50   Input ~ 0
17
Text GLabel 3225 2000 2    50   Input ~ 0
16
$Comp
L Connector_Generic:Conn_02x20_Odd_Even J6
U 1 1 5FF17591
P 7325 1800
F 0 "J6" H 7375 2717 50  0000 C CNN
F 1 "GPIO" H 7375 2626 50  0000 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_2x16_P2.54mm_Horizontal" H 7325 1800 50  0001 C CNN
F 3 "~" H 7325 1800 50  0001 C CNN
	1    7325 1800
	1    0    0    -1  
$EndComp
Text GLabel 3225 2100 2    50   Input ~ 0
04
Text GLabel 7125 1100 0    50   Input ~ 0
04
Text GLabel 7625 1100 2    50   Input ~ 0
05
Text GLabel 1725 2100 0    50   Input ~ 0
12
Text GLabel 7125 1500 0    50   Input ~ 0
12
Text GLabel 1725 2300 0    50   Input ~ 0
13
Text GLabel 7625 1500 2    50   Input ~ 0
13
Text GLabel 1725 2000 0    50   Input ~ 0
14
Text GLabel 7125 1600 0    50   Input ~ 0
14
Text GLabel 3225 2400 2    50   Input ~ 0
15
Text GLabel 7625 1600 2    50   Input ~ 0
15
Text GLabel 5025 2250 0    50   Input ~ 0
RXD
Text GLabel 7125 1700 0    50   Input ~ 0
16
Text GLabel 5025 2150 0    50   Input ~ 0
TXD
Text GLabel 7125 1800 0    50   Input ~ 0
18
Text GLabel 5025 2450 0    50   Input ~ 0
SDA
Text GLabel 7125 1900 0    50   Input ~ 0
21
Text GLabel 5025 2550 0    50   Input ~ 0
SCL
Text GLabel 3225 1100 2    50   Input ~ 0
22
Text GLabel 7125 2000 0    50   Input ~ 0
23
Text GLabel 1725 1700 0    50   Input ~ 0
25
Text GLabel 1725 1800 0    50   Input ~ 0
26
Text GLabel 7125 2100 0    50   Input ~ 0
26
Text GLabel 1725 1900 0    50   Input ~ 0
27
Text GLabel 1725 1500 0    50   Input ~ 0
32
Text GLabel 7125 2200 0    50   Input ~ 0
32
Text GLabel 1725 1600 0    50   Input ~ 0
33
Text GLabel 1725 1300 0    50   Input ~ 0
34
Text GLabel 1725 1400 0    50   Input ~ 0
35
Text GLabel 1725 1200 0    50   Input ~ 0
39
Text GLabel 1725 1100 0    50   Input ~ 0
36
Text GLabel 7125 2300 0    50   Input ~ 0
34
Text GLabel 7125 2400 0    50   Input ~ 0
36
Text GLabel 7625 2600 2    50   Input ~ 0
5V
Text GLabel 7125 2600 0    50   Input ~ 0
3V3
Text GLabel 7125 2700 0    50   Input ~ 0
3V3
$Comp
L power:GND #PWR09
U 1 1 5FF27599
P 7125 2800
F 0 "#PWR09" H 7125 2550 50  0001 C CNN
F 1 "GND" V 7130 2672 50  0000 R CNN
F 2 "" H 7125 2800 50  0001 C CNN
F 3 "" H 7125 2800 50  0001 C CNN
	1    7125 2800
	0    1    1    0   
$EndComp
$Comp
L power:GND #PWR012
U 1 1 5FF27FBE
P 7625 2800
F 0 "#PWR012" H 7625 2550 50  0001 C CNN
F 1 "GND" V 7630 2672 50  0000 R CNN
F 2 "" H 7625 2800 50  0001 C CNN
F 3 "" H 7625 2800 50  0001 C CNN
	1    7625 2800
	0    -1   -1   0   
$EndComp
$Comp
L Connector_Generic:Conn_01x02 J1
U 1 1 5FF288D5
P 4850 1050
F 0 "J1" H 4950 1000 50  0000 C CNN
F 1 "BTN1" H 4768 816 50  0001 C CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 4850 1050 50  0001 C CNN
F 3 "~" H 4850 1050 50  0001 C CNN
	1    4850 1050
	-1   0    0    1   
$EndComp
$Comp
L power:GND #PWR04
U 1 1 5FF2A461
P 5350 950
F 0 "#PWR04" H 5350 700 50  0001 C CNN
F 1 "GND" V 5355 822 50  0000 R CNN
F 2 "" H 5350 950 50  0001 C CNN
F 3 "" H 5350 950 50  0001 C CNN
	1    5350 950 
	0    -1   -1   0   
$EndComp
Text GLabel 5050 1075 3    50   Input ~ 0
BTN1
Wire Wire Line
	5350 950  5050 950 
$Comp
L Connector_Generic:Conn_01x02 J2
U 1 1 5FF3E2F0
P 4850 1400
F 0 "J2" H 4950 1350 50  0000 C CNN
F 1 "BTN2" H 4768 1166 50  0001 C CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 4850 1400 50  0001 C CNN
F 3 "~" H 4850 1400 50  0001 C CNN
	1    4850 1400
	-1   0    0    1   
$EndComp
$Comp
L Device:R R2
U 1 1 5FF3E2F6
P 5200 1400
F 0 "R2" V 5200 1400 50  0000 C CNN
F 1 "R" H 5084 1400 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5130 1400 50  0001 C CNN
F 3 "~" H 5200 1400 50  0001 C CNN
	1    5200 1400
	0    1    1    0   
$EndComp
$Comp
L power:GND #PWR05
U 1 1 5FF3E2FC
P 5350 1300
F 0 "#PWR05" H 5350 1050 50  0001 C CNN
F 1 "GND" V 5355 1172 50  0000 R CNN
F 2 "" H 5350 1300 50  0001 C CNN
F 3 "" H 5350 1300 50  0001 C CNN
	1    5350 1300
	0    -1   -1   0   
$EndComp
Text GLabel 5350 1400 2    50   Input ~ 0
3V3
Wire Wire Line
	5350 1300 5050 1300
Text GLabel 5050 1425 3    50   Input ~ 0
BTN2
Text GLabel 3225 1000 2    50   Input ~ 0
23
Text GLabel 3575 1000 2    50   Input ~ 0
V_MOSI
Text GLabel 3575 1100 2    50   Input ~ 0
SCL
$Comp
L power:PWR_FLAG #FLG01
U 1 1 5FF6110D
P 8625 1200
F 0 "#FLG01" H 8625 1275 50  0001 C CNN
F 1 "PWR_FLAG" V 8625 1500 50  0000 C CNN
F 2 "" H 8625 1200 50  0001 C CNN
F 3 "~" H 8625 1200 50  0001 C CNN
	1    8625 1200
	-1   0    0    1   
$EndComp
Wire Wire Line
	8625 1200 8775 1200
Text GLabel 4175 4825 1    50   Input ~ 0
Relay1
Text GLabel 4275 4825 1    50   Input ~ 0
Relay2
$Comp
L power:PWR_FLAG #FLG03
U 1 1 5FF69D11
P 10750 1250
F 0 "#FLG03" H 10750 1325 50  0001 C CNN
F 1 "PWR_FLAG" V 10750 1550 50  0001 C CNN
F 2 "" H 10750 1250 50  0001 C CNN
F 3 "~" H 10750 1250 50  0001 C CNN
	1    10750 1250
	-1   0    0    1   
$EndComp
$Comp
L power:PWR_FLAG #FLG02
U 1 1 5FF6A308
P 10750 1150
F 0 "#FLG02" H 10750 1225 50  0001 C CNN
F 1 "PWR_FLAG" V 10750 1450 50  0001 C CNN
F 2 "" H 10750 1150 50  0001 C CNN
F 3 "~" H 10750 1150 50  0001 C CNN
	1    10750 1150
	1    0    0    -1  
$EndComp
Text GLabel 3575 1700 2    50   Input ~ 0
V_CLK
Text GLabel 3575 1800 2    50   Input ~ 0
V_CS
Text GLabel 3575 1900 2    50   Input ~ 0
TXD
Text GLabel 3575 2000 2    50   Input ~ 0
RXD
Text GLabel 3575 1600 2    50   Input ~ 0
V_MISO
Text GLabel 3575 1400 2    50   Input ~ 0
SDA
Wire Wire Line
	3225 1000 3575 1000
Wire Wire Line
	3225 1100 3575 1100
Wire Wire Line
	3225 1400 3575 1400
Wire Wire Line
	3575 1600 3225 1600
Wire Wire Line
	3225 1700 3575 1700
Wire Wire Line
	3575 1800 3225 1800
Wire Wire Line
	3225 1900 3575 1900
Wire Wire Line
	3575 2000 3225 2000
Wire Notes Line width 12 style solid
	11150 1750 11150 650 
Wire Notes Line width 12 style solid
	11150 650  8200 650 
Wire Notes Line width 12 style solid
	8200 650  8200 1750
Wire Notes Line width 12 style solid
	8200 1750 11150 1750
Text Notes 8200 650  0    79   ~ 16
Power supply
Wire Notes Line width 12 style solid
	9150 1900 11150 1900
Wire Wire Line
	9500 3650 9500 3700
Wire Wire Line
	9850 3800 9700 3800
Wire Wire Line
	9700 3800 9700 3750
Wire Wire Line
	9700 3750 9600 3750
Wire Wire Line
	9600 3750 9600 3700
Wire Wire Line
	9600 3700 9500 3700
Connection ~ 9500 3700
Wire Wire Line
	9500 2550 9500 2600
Wire Wire Line
	9850 2700 9650 2700
Wire Wire Line
	9650 2700 9650 2650
Wire Wire Line
	9650 2650 9600 2650
Wire Wire Line
	9600 2650 9600 2600
Wire Wire Line
	9600 2600 9500 2600
Connection ~ 9500 2600
Wire Notes Line width 12 style solid
	9150 4250 11150 4250
Wire Notes Line width 12 style solid
	11150 1900 11150 4250
Wire Notes Line width 12 style solid
	9150 1900 9150 4250
Text Notes 9150 1900 0    79   ~ 16
Relays
Wire Notes Line width 12 style solid
	4650 650  4650 3000
Text Notes 4650 650  0    79   ~ 16
External connections
Wire Notes Line
	6750 650  6750 3000
Wire Notes Line width 12 style solid
	8000 650  8000 3000
Wire Notes Line width 12 style solid
	8000 3000 4650 3000
Wire Notes Line width 12 style solid
	8000 650  4650 650 
Wire Notes Line
	6750 1700 4650 1700
Text Notes 4700 1800 0    67   ~ 0
UEXT
Text Notes 4700 800  0    67   ~ 0
Buttons
Text GLabel 10850 2850 0    50   Input ~ 0
5V
Wire Wire Line
	10850 2950 10100 2950
Wire Wire Line
	10100 2950 10100 2800
Wire Wire Line
	10850 3050 10850 3300
Wire Wire Line
	11050 3300 11050 4050
Wire Wire Line
	11050 4050 10100 4050
Wire Wire Line
	10100 4050 10100 3900
Wire Wire Line
	10850 3300 11050 3300
Text GLabel 4725 2150 1    50   Input ~ 0
SDA
$Comp
L Device:R R7
U 1 1 5FFB4D47
P 4725 2300
F 0 "R7" V 4725 2300 50  0000 C CNN
F 1 "R" H 4609 2300 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 4655 2300 50  0001 C CNN
F 3 "~" H 4725 2300 50  0001 C CNN
	1    4725 2300
	-1   0    0    1   
$EndComp
Text GLabel 4725 2450 3    50   Input ~ 0
3V3
Wire Wire Line
	5050 1050 5050 1075
Wire Wire Line
	5050 1400 5050 1425
Connection ~ 5050 1400
$Comp
L Connector:Screw_Terminal_01x03 J13
U 1 1 5FECC764
P 10900 3500
F 0 "J13" H 10980 3496 50  0000 L CNN
F 1 "Screw_Terminal_01x03" H 10980 3451 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-3-5.08_1x03_P5.08mm_Horizontal" H 10900 3500 50  0001 C CNN
F 3 "~" H 10900 3500 50  0001 C CNN
	1    10900 3500
	1    0    0    1   
$EndComp
$Comp
L Connector:Screw_Terminal_01x03 J12
U 1 1 5FEBAABF
P 10900 2400
F 0 "J12" H 10980 2396 50  0000 L CNN
F 1 "Screw_Terminal_01x03" H 10980 2351 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-3-5.08_1x03_P5.08mm_Horizontal" H 10900 2400 50  0001 C CNN
F 3 "~" H 10900 2400 50  0001 C CNN
	1    10900 2400
	1    0    0    1   
$EndComp
$Comp
L power:GND #PWR019
U 1 1 601C1E53
P 10850 2750
F 0 "#PWR019" H 10850 2500 50  0001 C CNN
F 1 "GND" H 11000 2700 50  0001 C CNN
F 2 "" H 10850 2750 50  0001 C CNN
F 3 "" H 10850 2750 50  0001 C CNN
	1    10850 2750
	0    1    1    0   
$EndComp
$Comp
L Connector_Generic:Conn_01x04 J15
U 1 1 601BFCFE
P 11050 2850
F 0 "J15" H 11130 2842 50  0000 L CNN
F 1 "RelayConnector" H 10525 3075 50  0000 L CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x04_P2.54mm_Vertical" H 11050 2850 50  0001 C CNN
F 3 "~" H 11050 2850 50  0001 C CNN
	1    11050 2850
	1    0    0    -1  
$EndComp
$Comp
L Connector:Screw_Terminal_01x02 J30
U 1 1 6006F12D
P 9300 1100
F 0 "J30" V 9218 1180 50  0000 L CNN
F 1 "5V" V 9173 1180 50  0001 L CNN
F 2 "TerminalBlock_Phoenix:TerminalBlock_Phoenix_MKDS-1,5-2-5.08_1x02_P5.08mm_Horizontal" H 9300 1100 50  0001 C CNN
F 3 "~" H 9300 1100 50  0001 C CNN
	1    9300 1100
	-1   0    0    -1  
$EndComp
Wire Wire Line
	9500 1100 9600 1100
Wire Wire Line
	9600 1100 9600 1050
Wire Wire Line
	9500 1200 9600 1200
Wire Wire Line
	9600 1200 9600 1250
Connection ~ 9500 1200
Text GLabel 7125 900  0    50   Input ~ 0
00
Text GLabel 7625 900  2    50   Input ~ 0
01
Text GLabel 7125 1000 0    50   Input ~ 0
02
Text GLabel 7625 1000 2    50   Input ~ 0
03
Text GLabel 3225 1200 2    50   Input ~ 0
01
Text GLabel 3225 1300 2    50   Input ~ 0
03
Text GLabel 3225 2200 2    50   Input ~ 0
00
Text GLabel 3225 2300 2    50   Input ~ 0
02
Text GLabel 7125 1200 0    50   Input ~ 0
06
Text GLabel 7625 1200 2    50   Input ~ 0
07
Text GLabel 3225 2700 2    50   Input ~ 0
06
Text GLabel 3225 2600 2    50   Input ~ 0
07
Text GLabel 7125 1300 0    50   Input ~ 0
08
Text GLabel 3225 2500 2    50   Input ~ 0
08
Text GLabel 7625 1300 2    50   Input ~ 0
09
Text GLabel 7625 2700 2    50   Input ~ 0
3V3
Text GLabel 7625 2500 2    50   Input ~ 0
5V
Text GLabel 7625 2400 2    50   Input ~ 0
39
Text GLabel 7625 2300 2    50   Input ~ 0
35
Text GLabel 7625 2200 2    50   Input ~ 0
33
Text GLabel 7625 2100 2    50   Input ~ 0
27
Text GLabel 7625 2000 2    50   Input ~ 0
25
Text GLabel 7625 1800 2    50   Input ~ 0
19
Text GLabel 7625 1700 2    50   Input ~ 0
17
Text GLabel 7625 1900 2    50   Input ~ 0
22
Text GLabel 7625 1400 2    50   Input ~ 0
11
Text GLabel 1725 2600 0    50   Input ~ 0
11
Text GLabel 1725 2400 0    50   Input ~ 0
09
Text GLabel 7125 1400 0    50   Input ~ 0
10
Text GLabel 1725 2500 0    50   Input ~ 0
10
Text GLabel 7125 2500 0    50   Input ~ 0
EN
Text GLabel 1725 1000 0    50   Input ~ 0
EN
Connection ~ 5050 1050
Text GLabel 5350 1050 2    50   Input ~ 0
3V3
$Comp
L Device:R R1
U 1 1 5FF29DC2
P 5200 1050
F 0 "R1" V 5200 1050 50  0000 C CNN
F 1 "R" H 5084 1050 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5130 1050 50  0001 C CNN
F 3 "~" H 5200 1050 50  0001 C CNN
	1    5200 1050
	0    1    1    0   
$EndComp
$Comp
L Device:R R3
U 1 1 60619448
P 6300 1100
F 0 "R3" V 6300 1100 50  0000 C CNN
F 1 "R" H 6184 1100 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6230 1100 50  0001 C CNN
F 3 "~" H 6300 1100 50  0001 C CNN
	1    6300 1100
	-1   0    0    1   
$EndComp
$Comp
L Device:LED D3
U 1 1 6061C972
P 6300 1400
F 0 "D3" H 6350 1500 50  0000 C CNN
F 1 "LED" H 6225 1500 50  0000 C CNN
F 2 "LED_SMD:LED_0805_2012Metric" H 6300 1400 50  0001 C CNN
F 3 "~" H 6300 1400 50  0001 C CNN
	1    6300 1400
	0    -1   -1   0   
$EndComp
$Comp
L power:GND #PWR0101
U 1 1 6061E4EF
P 6300 1550
F 0 "#PWR0101" H 6300 1300 50  0001 C CNN
F 1 "GND" H 6525 1475 50  0000 R CNN
F 2 "" H 6300 1550 50  0001 C CNN
F 3 "" H 6300 1550 50  0001 C CNN
	1    6300 1550
	1    0    0    -1  
$EndComp
Text GLabel 6300 950  0    50   Input ~ 0
LED1
$Comp
L Device:R R4
U 1 1 60627230
P 6525 975
F 0 "R4" V 6525 975 50  0000 C CNN
F 1 "R" H 6409 975 50  0001 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6455 975 50  0001 C CNN
F 3 "~" H 6525 975 50  0001 C CNN
	1    6525 975 
	-1   0    0    1   
$EndComp
$Comp
L Device:LED D4
U 1 1 60627236
P 6525 1275
F 0 "D4" H 6550 1175 50  0000 C CNN
F 1 "LED" H 6425 1175 50  0000 C CNN
F 2 "LED_SMD:LED_0805_2012Metric" H 6525 1275 50  0001 C CNN
F 3 "~" H 6525 1275 50  0001 C CNN
	1    6525 1275
	0    -1   -1   0   
$EndComp
Text GLabel 6525 825  0    50   Input ~ 0
LED2
Wire Wire Line
	6525 1550 6300 1550
Wire Wire Line
	6525 1425 6525 1550
Connection ~ 6300 1550
Text GLabel 4375 4825 1    50   Input ~ 0
LED1
Text GLabel 4475 4825 1    50   Input ~ 0
LED2
Text GLabel 1525 2300 0    50   Input ~ 0
H_MOSI
Text GLabel 1525 2100 0    50   Input ~ 0
H_MISO
Text GLabel 1525 2000 0    50   Input ~ 0
H_CLK
Wire Wire Line
	1525 2300 1725 2300
Wire Wire Line
	1525 2100 1725 2100
Wire Wire Line
	1525 2000 1725 2000
$Comp
L Interface_Expansion:MCP23S17_SO U3
U 1 1 60D3D47F
P 2425 4625
F 0 "U3" H 2425 5906 50  0000 C CNN
F 1 "MCP23S17_SO" H 2425 5815 50  0000 C CNN
F 2 "Package_SO:SOIC-28W_7.5x17.9mm_P1.27mm" H 2625 3625 50  0001 L CNN
F 3 "http://ww1.microchip.com/downloads/en/DeviceDoc/20001952C.pdf" H 2625 3525 50  0001 L CNN
	1    2425 4625
	1    0    0    -1  
$EndComp
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP18
U 1 1 60E70C36
P 1525 5075
F 0 "JP18" V 1425 4950 50  0001 C CNN
F 1 " " V 1750 5725 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1525 5075 50  0001 C CNN
F 3 "~" H 1525 5075 50  0001 C CNN
	1    1525 5075
	0    -1   -1   0   
$EndComp
Text GLabel 1375 5025 0    50   Input ~ 0
3V3
$Comp
L power:GND #PWR0102
U 1 1 60E7CD18
P 1375 5125
F 0 "#PWR0102" H 1375 4875 50  0001 C CNN
F 1 "GND" H 1380 4952 50  0001 C CNN
F 2 "" H 1375 5125 50  0001 C CNN
F 3 "" H 1375 5125 50  0001 C CNN
	1    1375 5125
	1    0    0    -1  
$EndComp
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP19
U 1 1 60E7DCF1
P 1525 5325
F 0 "JP19" V 1425 5200 50  0001 C CNN
F 1 " " V 1750 5975 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1525 5325 50  0001 C CNN
F 3 "~" H 1525 5325 50  0001 C CNN
	1    1525 5325
	0    -1   -1   0   
$EndComp
Text GLabel 1375 5275 0    50   Input ~ 0
3V3
$Comp
L power:GND #PWR0103
U 1 1 60E7DCF8
P 1375 5375
F 0 "#PWR0103" H 1375 5125 50  0001 C CNN
F 1 "GND" H 1380 5202 50  0001 C CNN
F 2 "" H 1375 5375 50  0001 C CNN
F 3 "" H 1375 5375 50  0001 C CNN
	1    1375 5375
	1    0    0    -1  
$EndComp
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP20
U 1 1 60E8A56B
P 1525 5575
F 0 "JP20" V 1425 5450 50  0001 C CNN
F 1 " " V 1750 6225 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1525 5575 50  0001 C CNN
F 3 "~" H 1525 5575 50  0001 C CNN
	1    1525 5575
	0    -1   -1   0   
$EndComp
Text GLabel 1375 5525 0    50   Input ~ 0
3V3
$Comp
L power:GND #PWR0104
U 1 1 60E8A572
P 1375 5625
F 0 "#PWR0104" H 1375 5375 50  0001 C CNN
F 1 "GND" H 1380 5452 50  0001 C CNN
F 2 "" H 1375 5625 50  0001 C CNN
F 3 "" H 1375 5625 50  0001 C CNN
	1    1375 5625
	1    0    0    -1  
$EndComp
Wire Wire Line
	1675 5075 1725 5075
Wire Wire Line
	1725 5075 1725 5225
Wire Wire Line
	1675 5575 1725 5575
Wire Wire Line
	1725 5575 1725 5425
$Comp
L power:GND #PWR0105
U 1 1 60ED14E0
P 2425 5725
F 0 "#PWR0105" H 2425 5475 50  0001 C CNN
F 1 "GND" H 2430 5552 50  0001 C CNN
F 2 "" H 2425 5725 50  0001 C CNN
F 3 "" H 2425 5725 50  0001 C CNN
	1    2425 5725
	1    0    0    -1  
$EndComp
Text GLabel 2425 3525 0    50   Input ~ 0
3V3
$Comp
L power:PWR_FLAG #FLG0104
U 1 1 610B3245
P 1675 5075
F 0 "#FLG0104" H 1675 5150 50  0001 C CNN
F 1 "PWR_FLAG" H 1425 5125 50  0001 C CNN
F 2 "" H 1675 5075 50  0001 C CNN
F 3 "~" H 1675 5075 50  0001 C CNN
	1    1675 5075
	-1   0    0    -1  
$EndComp
$Comp
L power:PWR_FLAG #FLG0105
U 1 1 610B35E7
P 1675 5325
F 0 "#FLG0105" H 1675 5400 50  0001 C CNN
F 1 "PWR_FLAG" H 1425 5375 50  0001 C CNN
F 2 "" H 1675 5325 50  0001 C CNN
F 3 "~" H 1675 5325 50  0001 C CNN
	1    1675 5325
	-1   0    0    -1  
$EndComp
$Comp
L power:PWR_FLAG #FLG0106
U 1 1 610B3A50
P 1675 5575
F 0 "#FLG0106" H 1675 5650 50  0001 C CNN
F 1 "PWR_FLAG" H 1425 5625 50  0001 C CNN
F 2 "" H 1675 5575 50  0001 C CNN
F 3 "~" H 1675 5575 50  0001 C CNN
	1    1675 5575
	-1   0    0    -1  
$EndComp
Connection ~ 1675 5325
Connection ~ 1675 5575
Connection ~ 1675 5075
Wire Wire Line
	1675 5325 1725 5325
Text GLabel 7475 4625 2    50   Input ~ 0
H_CLK
Text GLabel 7475 4725 2    50   Input ~ 0
V_CLK
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP9
U 1 1 608FE91D
P 7325 3625
F 0 "JP9" V 7200 3625 50  0000 C CNN
F 1 " " V 7550 4275 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 7325 3625 50  0001 C CNN
F 3 "~" H 7325 3625 50  0001 C CNN
	1    7325 3625
	0    1    1    0   
$EndComp
Wire Wire Line
	7175 3975 7100 3975
Wire Wire Line
	7175 4325 7050 4325
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP11
U 1 1 608D2B62
P 7325 4325
F 0 "JP11" V 7225 4200 50  0000 C CNN
F 1 " " V 7550 4975 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 7325 4325 50  0001 C CNN
F 3 "~" H 7325 4325 50  0001 C CNN
	1    7325 4325
	0    1    1    0   
$EndComp
Text GLabel 7475 3575 2    50   Input ~ 0
H_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP12
U 1 1 608C32DA
P 7325 4675
F 0 "JP12" V 7225 4550 50  0000 C CNN
F 1 " " V 7550 5325 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 7325 4675 50  0001 C CNN
F 3 "~" H 7325 4675 50  0001 C CNN
	1    7325 4675
	0    1    1    0   
$EndComp
Wire Wire Line
	6975 4675 7175 4675
Wire Wire Line
	6975 3925 6975 4675
Wire Wire Line
	6550 3925 6975 3925
Wire Wire Line
	7050 3825 7050 4325
Wire Wire Line
	6550 3825 7050 3825
Wire Wire Line
	7100 3725 7100 3975
Wire Wire Line
	6550 3725 7100 3725
Wire Wire Line
	7175 3625 6550 3625
$Comp
L Jumper:SolderJumper_2_Open JP4
U 1 1 606434FC
P 6800 4900
F 0 "JP4" H 6800 4800 50  0001 C CNN
F 1 " " H 6800 5014 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 6800 4900 50  0001 C CNN
F 3 "~" H 6800 4900 50  0001 C CNN
	1    6800 4900
	0    1    1    0   
$EndComp
$Comp
L Device:R R8
U 1 1 60643502
P 6900 5200
F 0 "R8" V 6825 5150 50  0001 C CNN
F 1 "R" V 6900 5125 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6830 5200 50  0001 C CNN
F 3 "~" H 6900 5200 50  0001 C CNN
	1    6900 5200
	-1   0    0    -1  
$EndComp
$Comp
L Device:R R13
U 1 1 60643508
P 6900 4900
F 0 "R13" V 6825 4850 50  0001 C CNN
F 1 "R" V 6900 4825 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6830 4900 50  0001 C CNN
F 3 "~" H 6900 4900 50  0001 C CNN
	1    6900 4900
	-1   0    0    -1  
$EndComp
Wire Wire Line
	6900 5050 6800 5050
Connection ~ 6900 5050
Text GLabel 6800 5050 3    50   Input ~ 0
AI1
Text GLabel 6225 5350 1    50   Input ~ 0
AD_GND
Text GLabel 6700 5050 3    50   Input ~ 0
AI3
Connection ~ 6600 5050
Wire Wire Line
	6600 5050 6700 5050
$Comp
L Device:R R15
U 1 1 6065627F
P 6600 4900
F 0 "R15" V 6525 4850 50  0001 C CNN
F 1 "R" V 6600 4825 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6530 4900 50  0001 C CNN
F 3 "~" H 6600 4900 50  0001 C CNN
	1    6600 4900
	-1   0    0    -1  
$EndComp
$Comp
L Device:R R12
U 1 1 60656279
P 6600 5200
F 0 "R12" V 6525 5150 50  0001 C CNN
F 1 "R" V 6600 5125 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 6530 5200 50  0001 C CNN
F 3 "~" H 6600 5200 50  0001 C CNN
	1    6600 5200
	-1   0    0    -1  
$EndComp
$Comp
L Jumper:SolderJumper_2_Open JP8
U 1 1 60656273
P 6700 4900
F 0 "JP8" H 6700 5000 50  0001 C CNN
F 1 " " H 6700 5014 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 6700 4900 50  0001 C CNN
F 3 "~" H 6700 4900 50  0001 C CNN
	1    6700 4900
	0    1    1    0   
$EndComp
Text GLabel 5650 3925 0    50   Input ~ 0
AI3
Text GLabel 5650 3825 0    50   Input ~ 0
AI2
Text GLabel 5650 3725 0    50   Input ~ 0
AI1
Text GLabel 5650 3625 0    50   Input ~ 0
AI0
Text GLabel 5625 5050 3    50   Input ~ 0
AI0
Text GLabel 7850 5900 2    50   Input ~ 0
3V3
Text GLabel 6100 3425 1    50   Input ~ 0
VREF
Text GLabel 6000 3425 1    50   Input ~ 0
3V3
Text GLabel 7850 5800 2    50   Input ~ 0
5V
Text GLabel 7475 3675 2    50   Input ~ 0
V_CS
Text GLabel 7475 4275 2    50   Input ~ 0
H_MISO
Text GLabel 7475 4375 2    50   Input ~ 0
V_MISO
Text GLabel 7475 4025 2    50   Input ~ 0
V_MOSI
Text GLabel 7475 3925 2    50   Input ~ 0
H_MOSI
Text GLabel 5900 4400 0    50   Input ~ 0
AD_GND
$Comp
L power:GND #PWR08
U 1 1 60707826
P 6000 4325
F 0 "#PWR08" H 6000 4075 50  0001 C CNN
F 1 "GND" H 6005 4152 50  0001 C CNN
F 2 "" H 6000 4325 50  0001 C CNN
F 3 "" H 6000 4325 50  0001 C CNN
	1    6000 4325
	1    0    0    -1  
$EndComp
Text GLabel 5975 4750 3    50   Input ~ 0
VREF
Text GLabel 6475 4750 3    50   Input ~ 0
AD_GND
Text GLabel 5725 5050 3    50   Input ~ 0
AI2
Connection ~ 5625 4750
Wire Wire Line
	6700 4750 6600 4750
Connection ~ 5850 5050
Wire Wire Line
	5850 5050 5725 5050
Wire Wire Line
	5725 4750 5850 4750
$Comp
L Device:R R14
U 1 1 60646EFA
P 5850 4900
F 0 "R14" V 5775 4850 50  0001 C CNN
F 1 "R" V 5850 4825 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5780 4900 50  0001 C CNN
F 3 "~" H 5850 4900 50  0001 C CNN
	1    5850 4900
	1    0    0    -1  
$EndComp
$Comp
L Device:R R11
U 1 1 60646EF4
P 5850 5200
F 0 "R11" V 5775 5150 50  0001 C CNN
F 1 "R" V 5850 5125 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5780 5200 50  0001 C CNN
F 3 "~" H 5850 5200 50  0001 C CNN
	1    5850 5200
	1    0    0    -1  
$EndComp
$Comp
L Jumper:SolderJumper_2_Open JP7
U 1 1 60646EEE
P 5725 4900
F 0 "JP7" H 5725 5000 50  0001 C CNN
F 1 " " H 5725 5014 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 5725 4900 50  0001 C CNN
F 3 "~" H 5725 4900 50  0001 C CNN
	1    5725 4900
	0    -1   1    0   
$EndComp
Wire Wire Line
	6800 4750 6900 4750
$Comp
L Connector_Generic:Conn_02x03_Odd_Even J4
U 1 1 60774238
P 6175 4650
F 0 "J4" H 6225 5067 50  0001 C CNN
F 1 "AD Conn" H 6250 4450 50  0000 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_2x03_P2.54mm_Vertical" H 6175 4650 50  0001 C CNN
F 3 "~" H 6175 4650 50  0001 C CNN
	1    6175 4650
	1    0    0    -1  
$EndComp
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP17
U 1 1 60D601FF
P 850 3675
F 0 "JP17" V 750 3550 50  0000 C CNN
F 1 " " V 1075 4325 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 850 3675 50  0001 C CNN
F 3 "~" H 850 3675 50  0001 C CNN
	1    850  3675
	1    0    0    -1  
$EndComp
Text GLabel 800  3525 1    50   Input ~ 0
H_MISO
Text GLabel 900  3525 1    50   Input ~ 0
V_MISO
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP16
U 1 1 60D48BE4
P 1100 3675
F 0 "JP16" V 1000 3550 50  0000 C CNN
F 1 " " V 1325 4325 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1100 3675 50  0001 C CNN
F 3 "~" H 1100 3675 50  0001 C CNN
	1    1100 3675
	1    0    0    -1  
$EndComp
Text GLabel 1150 3525 1    50   Input ~ 0
V_MOSI
Text GLabel 1050 3525 1    50   Input ~ 0
H_MOSI
Text GLabel 1300 3525 1    50   Input ~ 0
H_CLK
Text GLabel 1400 3525 1    50   Input ~ 0
V_CLK
Text GLabel 1550 3525 1    50   Input ~ 0
H_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP14
U 1 1 60D588E4
P 1600 3675
F 0 "JP14" V 1500 3550 50  0000 C CNN
F 1 " " V 1825 4325 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1600 3675 50  0001 C CNN
F 3 "~" H 1600 3675 50  0001 C CNN
	1    1600 3675
	1    0    0    -1  
$EndComp
Text GLabel 1650 3525 1    50   Input ~ 0
V_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP15
U 1 1 60D40A91
P 1350 3675
F 0 "JP15" V 1250 3550 50  0000 C CNN
F 1 " " V 1575 4325 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 1350 3675 50  0001 C CNN
F 3 "~" H 1350 3675 50  0001 C CNN
	1    1350 3675
	1    0    0    -1  
$EndComp
Wire Wire Line
	1600 3825 1725 3825
Wire Wire Line
	1350 3825 1350 3925
Wire Wire Line
	1100 3825 1100 4025
Wire Wire Line
	1100 4025 1725 4025
Wire Wire Line
	850  3825 850  4125
Wire Wire Line
	1350 3925 1725 3925
Text GLabel 7550 6225 0    50   Input ~ 0
AD_GND
$Comp
L power:GND #PWR07
U 1 1 6068325F
P 7850 6225
F 0 "#PWR07" H 7850 5975 50  0001 C CNN
F 1 "GND" H 7855 6052 50  0001 C CNN
F 2 "" H 7850 6225 50  0001 C CNN
F 3 "" H 7850 6225 50  0001 C CNN
	1    7850 6225
	1    0    0    -1  
$EndComp
Text GLabel 7550 5850 0    50   Input ~ 0
VREF
$Comp
L Jumper:SolderJumper_2_Open JP3
U 1 1 606829F3
P 7700 6225
F 0 "JP3" H 7700 6325 50  0000 C CNN
F 1 " " H 7700 6339 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 7700 6225 50  0001 C CNN
F 3 "~" H 7700 6225 50  0001 C CNN
	1    7700 6225
	1    0    0    1   
$EndComp
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP2
U 1 1 608DF3C9
P 7700 5850
F 0 "JP2" V 7600 5725 50  0000 C CNN
F 1 " " V 7925 6500 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 7700 5850 50  0001 C CNN
F 3 "~" H 7700 5850 50  0001 C CNN
	1    7700 5850
	0    1    -1   0   
$EndComp
$Comp
L power:PWR_FLAG #FLG0101
U 1 1 6090638D
P 7550 5725
F 0 "#FLG0101" H 7550 5800 50  0001 C CNN
F 1 "PWR_FLAG" H 7300 5775 50  0000 C CNN
F 2 "" H 7550 5725 50  0001 C CNN
F 3 "~" H 7550 5725 50  0001 C CNN
	1    7550 5725
	-1   0    0    -1  
$EndComp
$Comp
L power:PWR_FLAG #FLG0102
U 1 1 60918C3E
P 7550 6125
F 0 "#FLG0102" H 7550 6200 50  0001 C CNN
F 1 "PWR_FLAG" H 7300 6175 50  0000 C CNN
F 2 "" H 7550 6125 50  0001 C CNN
F 3 "~" H 7550 6125 50  0001 C CNN
	1    7550 6125
	-1   0    0    -1  
$EndComp
Wire Wire Line
	7550 6125 7550 6225
Wire Wire Line
	7550 5725 7550 5850
Wire Wire Line
	6900 5350 6600 5350
Connection ~ 6700 4750
Wire Wire Line
	6475 4650 6700 4650
Wire Wire Line
	6700 4650 6700 4750
Wire Wire Line
	6800 4750 6800 4550
Wire Wire Line
	6800 4550 6475 4550
Connection ~ 6800 4750
$Comp
L Jumper:SolderJumper_2_Open JP1
U 1 1 606BA4FD
P 5625 4900
F 0 "JP1" H 5625 5000 50  0001 C CNN
F 1 " " H 5625 5014 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 5625 4900 50  0001 C CNN
F 3 "~" H 5625 4900 50  0001 C CNN
	1    5625 4900
	0    -1   1    0   
$EndComp
$Comp
L Device:R R9
U 1 1 606C669A
P 5525 4900
F 0 "R9" V 5450 4850 50  0001 C CNN
F 1 "R" V 5525 4825 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5455 4900 50  0001 C CNN
F 3 "~" H 5525 4900 50  0001 C CNN
	1    5525 4900
	1    0    0    -1  
$EndComp
Wire Wire Line
	5625 4750 5525 4750
$Comp
L Device:R R10
U 1 1 606C73FE
P 5525 5200
F 0 "R10" V 5450 5150 50  0001 C CNN
F 1 "R" V 5525 5125 50  0000 C CNN
F 2 "Resistor_SMD:R_0805_2012Metric" V 5455 5200 50  0001 C CNN
F 3 "~" H 5525 5200 50  0001 C CNN
	1    5525 5200
	1    0    0    -1  
$EndComp
Connection ~ 5525 5050
Wire Wire Line
	5525 5050 5625 5050
Wire Wire Line
	5725 4750 5725 4650
Connection ~ 5725 4750
Wire Wire Line
	5975 4650 5725 4650
Wire Wire Line
	5975 4550 5625 4550
Wire Wire Line
	5625 4550 5625 4750
Wire Wire Line
	5525 5350 5850 5350
Connection ~ 5850 5350
Wire Wire Line
	5850 5350 6600 5350
Connection ~ 6600 5350
NoConn ~ 1725 4625
Wire Wire Line
	4100 4525 4100 5425
Wire Wire Line
	3125 4525 4100 4525
Wire Wire Line
	4050 4425 4050 5325
Wire Wire Line
	3125 4425 4050 4425
Wire Wire Line
	4000 4325 4000 5225
Wire Wire Line
	3125 4325 4000 4325
Wire Wire Line
	3950 4225 3950 5125
Wire Wire Line
	3125 4225 3950 4225
Wire Wire Line
	3900 4125 3900 5025
Wire Wire Line
	3125 4125 3900 4125
Wire Wire Line
	3850 4025 3850 4925
Wire Wire Line
	3125 4025 3850 4025
Wire Wire Line
	3800 3925 3800 4825
Wire Wire Line
	3125 3925 3800 3925
Wire Wire Line
	3750 3825 3750 4725
Wire Wire Line
	3125 3825 3750 3825
Wire Wire Line
	3750 4725 3675 4725
Wire Wire Line
	3175 5425 3125 5425
Wire Wire Line
	3125 5325 3175 5325
Wire Wire Line
	3175 5225 3125 5225
Wire Wire Line
	3125 5125 3175 5125
Wire Wire Line
	3175 5025 3125 5025
Wire Wire Line
	3125 4925 3175 4925
Wire Wire Line
	3175 4825 3125 4825
Wire Wire Line
	4100 5425 3675 5425
Wire Wire Line
	4050 5325 3675 5325
Wire Wire Line
	4000 5225 3675 5225
Wire Wire Line
	3950 5125 3675 5125
Wire Wire Line
	3900 5025 3675 5025
Wire Wire Line
	3850 4925 3675 4925
Wire Wire Line
	3800 4825 3675 4825
Wire Wire Line
	3125 4725 3175 4725
$Comp
L Connector_Generic:Conn_02x08_Top_Bottom J17
U 1 1 60DAD2A6
P 3375 5025
F 0 "J17" H 3425 5542 50  0001 C CNN
F 1 "IOExt_Header" H 3425 5450 50  0000 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_2x08_P2.54mm_Vertical" H 3375 5025 50  0001 C CNN
F 3 "~" H 3375 5025 50  0001 C CNN
	1    3375 5025
	1    0    0    -1  
$EndComp
Connection ~ 4100 5425
Connection ~ 4050 5325
Wire Wire Line
	1525 1300 1725 1300
Wire Wire Line
	1525 1400 1725 1400
Text GLabel 1525 1300 0    50   Input ~ 0
BTN1
Text GLabel 1525 1400 0    50   Input ~ 0
BTN2
Connection ~ 3950 5125
Connection ~ 4000 5225
Text GLabel 3600 2400 2    50   Input ~ 0
H_CS
Wire Wire Line
	3600 2400 3225 2400
$Comp
L ESP32-DevKit:MCP3302 U2
U 1 1 6081C0E2
P 6100 3825
F 0 "U2" H 5775 4175 50  0000 C CNN
F 1 "MCP3302" H 6300 4175 50  0000 C CNN
F 2 "Package_SO:SOIC-14_3.9x8.7mm_P1.27mm" H 5750 4125 50  0001 C CNN
F 3 "" H 5750 4125 50  0001 C CNN
	1    6100 3825
	1    0    0    -1  
$EndComp
Wire Wire Line
	5900 4325 5900 4400
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP5
U 1 1 60A61CF9
P 6275 2000
F 0 "JP5" V 6175 1875 50  0000 C CNN
F 1 " " V 6500 2650 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 6275 2000 50  0001 C CNN
F 3 "~" H 6275 2000 50  0001 C CNN
	1    6275 2000
	0    1    1    0   
$EndComp
Text GLabel 6425 1950 2    50   Input ~ 0
H_MISO
Text GLabel 6425 2050 2    50   Input ~ 0
V_MISO
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP6
U 1 1 60A61D01
P 6275 2250
F 0 "JP6" V 6175 2125 50  0000 C CNN
F 1 " " V 6500 2900 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 6275 2250 50  0001 C CNN
F 3 "~" H 6275 2250 50  0001 C CNN
	1    6275 2250
	0    1    1    0   
$EndComp
Text GLabel 6425 2300 2    50   Input ~ 0
V_MOSI
Text GLabel 6425 2200 2    50   Input ~ 0
H_MOSI
Text GLabel 6425 2450 2    50   Input ~ 0
H_CLK
Text GLabel 6425 2550 2    50   Input ~ 0
V_CLK
Text GLabel 6425 2700 2    50   Input ~ 0
H_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP21
U 1 1 60A61D0C
P 6275 2750
F 0 "JP21" V 6175 2625 50  0000 C CNN
F 1 " " V 6500 3400 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 6275 2750 50  0001 C CNN
F 3 "~" H 6275 2750 50  0001 C CNN
	1    6275 2750
	0    1    1    0   
$EndComp
Text GLabel 6425 2800 2    50   Input ~ 0
V_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP13
U 1 1 60A61D13
P 6275 2500
F 0 "JP13" V 6175 2375 50  0000 C CNN
F 1 " " V 6500 3150 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 6275 2500 50  0001 C CNN
F 3 "~" H 6275 2500 50  0001 C CNN
	1    6275 2500
	0    1    1    0   
$EndComp
Wire Wire Line
	6025 2150 6025 2000
Wire Wire Line
	6025 2000 6125 2000
Wire Wire Line
	6125 2250 6025 2250
Wire Wire Line
	6025 2350 6125 2350
Wire Wire Line
	6125 2350 6125 2500
Wire Wire Line
	6025 2450 6025 2750
Wire Wire Line
	6025 2750 6125 2750
Text Notes 5350 3150 0    79   ~ 16
AD Converter
Wire Notes Line width 12 style solid
	8000 3150 8000 5475
Wire Notes Line width 12 style solid
	5350 3150 5350 5475
Wire Notes Line width 12 style solid
	8000 5475 5350 5475
Wire Notes Line width 12 style solid
	5350 3150 8000 3150
Text Notes 675  3150 0    79   ~ 16
I/O Expander
Wire Notes Line width 12 style solid
	4575 3150 4575 5825
Wire Notes Line width 12 style solid
	4575 5825 675  5825
Wire Notes Line width 12 style solid
	675  3150 675  5825
Wire Notes Line width 12 style solid
	675  3150 4575 3150
Text Notes 9525 5925 0    67   ~ 0
Patch connector
Wire Wire Line
	10450 5650 10250 5650
Wire Wire Line
	10050 5050 10050 5075
Wire Wire Line
	9950 5075 9950 5150
Wire Wire Line
	9950 5075 10050 5075
Wire Wire Line
	10150 5050 10150 5100
Wire Wire Line
	10050 5100 10050 5150
Wire Wire Line
	10050 5100 10150 5100
Wire Wire Line
	10250 5050 10250 5125
Wire Wire Line
	10150 5125 10150 5150
Wire Wire Line
	10150 5125 10250 5125
Wire Wire Line
	10350 5050 10350 5150
Wire Wire Line
	10250 5150 10350 5150
Text Notes 8425 4550 0    67   ~ 0
Onboard connector
Text Notes 9525 4550 0    67   ~ 0
Adaptor card
Text Notes 8375 4400 0    79   ~ 16
TFT screen connection
Text GLabel 9875 6225 0    50   Input ~ 0
TFT_Y+
Text GLabel 9875 6125 0    50   Input ~ 0
TFT_Y-
Text GLabel 10375 6225 2    50   Input ~ 0
TFT_X+
Text GLabel 10375 6125 2    50   Input ~ 0
TFT_X-
Text GLabel 10375 6025 2    50   Input ~ 0
TFT_DC
Text GLabel 9875 6025 0    50   Input ~ 0
TFT_RST
$Comp
L Connector_Generic:Conn_02x03_Odd_Even J19
U 1 1 60ABF558
P 10075 6125
F 0 "J19" H 9950 5875 50  0001 C CNN
F 1 "Conn_02x04_Odd_Even" H 10125 6351 50  0001 C CNN
F 2 "Connector_PinHeader_1.27mm:PinHeader_2x03_P1.27mm_Vertical" H 10075 6125 50  0001 C CNN
F 3 "~" H 10075 6125 50  0001 C CNN
	1    10075 6125
	1    0    0    -1  
$EndComp
Text GLabel 9150 5725 1    50   Input ~ 0
TFT_CS
Wire Wire Line
	11050 5775 11050 5050
Wire Wire Line
	9750 5775 11050 5775
Wire Wire Line
	9750 5650 9750 5775
Wire Wire Line
	10950 5750 10950 5050
Wire Wire Line
	9850 5750 10950 5750
Wire Wire Line
	9850 5650 9850 5750
Wire Wire Line
	10850 5725 10850 5050
Wire Wire Line
	9950 5725 10850 5725
Wire Wire Line
	9950 5650 9950 5725
Wire Wire Line
	10750 5700 10750 5050
Wire Wire Line
	10050 5700 10750 5700
Wire Wire Line
	10050 5650 10050 5700
Wire Wire Line
	10150 5675 10550 5675
Wire Wire Line
	10150 5650 10150 5675
Wire Wire Line
	9750 5050 9750 5150
Wire Wire Line
	9850 5050 9850 5150
Text Notes 11075 4775 1    50   ~ 0
X-
Text Notes 10975 4775 1    50   ~ 0
Y-
Text Notes 10875 4775 1    50   ~ 0
X+
Text Notes 10775 4775 1    50   ~ 0
Y+
Text Notes 10675 4775 1    50   ~ 0
Lite
Text Notes 10075 4775 1    50   ~ 0
CLK
Text Notes 10575 4775 1    50   ~ 0
RST
Text Notes 10475 4775 1    50   ~ 0
D/C
Text Notes 10375 4775 1    50   ~ 0
CS
Text Notes 10275 4775 1    50   ~ 0
MOSI
Text Notes 10175 4775 1    50   ~ 0
MISO
Text Notes 9975 4775 1    50   ~ 0
3VOut
Text Notes 9875 4775 1    50   ~ 0
Vin
Text Notes 9775 4775 1    50   ~ 0
GND
NoConn ~ 10650 5050
Wire Wire Line
	10550 5050 10550 5675
Wire Wire Line
	10450 5050 10450 5650
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP25
U 1 1 60B5E777
P 9150 5875
F 0 "JP25" V 9050 5750 50  0001 C CNN
F 1 " " V 9375 6525 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 9150 5875 50  0001 C CNN
F 3 "~" H 9150 5875 50  0001 C CNN
	1    9150 5875
	1    0    0    1   
$EndComp
Text GLabel 9200 6025 3    50   Input ~ 0
V_CS
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP22
U 1 1 60B5E77E
P 8550 5875
F 0 "JP22" V 8450 5750 50  0001 C CNN
F 1 " " V 8775 6525 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 8550 5875 50  0001 C CNN
F 3 "~" H 8550 5875 50  0001 C CNN
	1    8550 5875
	1    0    0    1   
$EndComp
Text GLabel 9100 6025 3    50   Input ~ 0
H_CS
Text GLabel 8600 6025 3    50   Input ~ 0
V_CLK
Text GLabel 8500 6025 3    50   Input ~ 0
H_CLK
Text GLabel 8900 6025 3    50   Input ~ 0
H_MOSI
Text GLabel 9000 6025 3    50   Input ~ 0
V_MOSI
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP24
U 1 1 60B5E76C
P 8950 5875
F 0 "JP24" V 8850 5750 50  0001 C CNN
F 1 " " V 9175 6525 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 8950 5875 50  0001 C CNN
F 3 "~" H 8950 5875 50  0001 C CNN
	1    8950 5875
	1    0    0    1   
$EndComp
Text GLabel 8800 6025 3    50   Input ~ 0
V_MISO
Text GLabel 8700 6025 3    50   Input ~ 0
H_MISO
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP23
U 1 1 60B5E764
P 8750 5875
F 0 "JP23" V 8650 5750 50  0001 C CNN
F 1 " " V 8975 6525 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 8750 5875 50  0001 C CNN
F 3 "~" H 8750 5875 50  0001 C CNN
	1    8750 5875
	1    0    0    1   
$EndComp
$Comp
L Connector_Generic:Conn_01x14 J11
U 1 1 5FEFA803
P 10350 4850
F 0 "J11" V 10350 4025 50  0001 C CNN
F 1 "Conn_01x14" V 10476 4796 50  0001 C CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_2x02_P2.54mm_Vertical" H 10350 4850 50  0001 C CNN
F 3 "~" H 10350 4850 50  0001 C CNN
	1    10350 4850
	0    -1   -1   0   
$EndComp
$Comp
L Connector_Generic:Conn_02x06_Odd_Even J10
U 1 1 5FEFE287
P 9950 5350
F 0 "J10" V 10000 4962 50  0000 R CNN
F 1 "Conn_02x06_Odd_Even" V 10045 4962 50  0001 R CNN
F 2 "Connector_IDC:IDC-Header_2x06_P2.54mm_Vertical" H 9950 5350 50  0001 C CNN
F 3 "~" H 9950 5350 50  0001 C CNN
	1    9950 5350
	0    -1   1    0   
$EndComp
NoConn ~ 9950 5050
Text GLabel 8650 4900 1    50   Input ~ 0
TFT_Y-
Text GLabel 8550 4900 1    50   Input ~ 0
TFT_X-
Text GLabel 8950 4900 1    50   Input ~ 0
TFT_RST
Text GLabel 8850 4900 1    50   Input ~ 0
TFT_Y+
Text GLabel 8750 4900 1    50   Input ~ 0
TFT_X+
Text GLabel 9050 4900 1    50   Input ~ 0
TFT_DC
$Comp
L power:GND #PWR014
U 1 1 5FEF6713
P 8550 5400
F 0 "#PWR014" H 8550 5150 50  0001 C CNN
F 1 "GND" V 8555 5272 50  0001 R CNN
F 2 "" H 8550 5400 50  0001 C CNN
F 3 "" H 8550 5400 50  0001 C CNN
	1    8550 5400
	1    0    0    -1  
$EndComp
Text GLabel 8650 5400 3    50   Input ~ 0
3V3
$Comp
L Connector_Generic:Conn_02x06_Odd_Even J8
U 1 1 5FEE7DE7
P 8750 5200
F 0 "J8" V 8800 4800 50  0000 C CNN
F 1 "Screen connector" H 8800 5526 50  0000 C CNN
F 2 "Connector_IDC:IDC-Header_2x06_P2.54mm_Vertical" H 8750 5200 50  0001 C CNN
F 3 "~" H 8750 5200 50  0001 C CNN
	1    8750 5200
	0    -1   -1   0   
$EndComp
Wire Notes Line
	9475 4400 9475 6450
Wire Notes Line width 12 style solid
	11150 4400 11150 6450
Wire Notes Line width 12 style solid
	8375 4400 8375 6450
Wire Notes Line width 12 style solid
	8375 4400 11150 4400
Wire Notes Line width 12 style solid
	11150 6450 8375 6450
Wire Notes Line
	9475 5800 11150 5800
Wire Wire Line
	8750 5650 8550 5650
Wire Wire Line
	8550 5650 8550 5725
Wire Wire Line
	8750 5400 8750 5650
Wire Wire Line
	8850 5725 8750 5725
Wire Wire Line
	8850 5400 8850 5725
Wire Wire Line
	8950 5400 8950 5725
Wire Wire Line
	9050 5400 9050 5725
Wire Wire Line
	9050 5725 9150 5725
$Comp
L ESP32-DevKit:SolderJumper_3_Open_Slim JP10
U 1 1 608D1001
P 7325 3975
F 0 "JP10" V 7225 3850 50  0000 C CNN
F 1 " " V 7550 4625 50  0001 C CNN
F 2 "Jumper:SolderJumper-3_P1.3mm_Open_RoundedPad1.0x1.5mm" H 7325 3975 50  0001 C CNN
F 3 "~" H 7325 3975 50  0001 C CNN
	1    7325 3975
	0    1    1    0   
$EndComp
Text GLabel 7175 3625 1    50   Input ~ 0
AD_CS
$Comp
L Jumper:SolderJumper_2_Open JP26
U 1 1 60CEE6E8
P 4175 4975
F 0 "JP26" H 4175 5075 50  0001 C CNN
F 1 " " H 4175 5089 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 4175 4975 50  0001 C CNN
F 3 "~" H 4175 4975 50  0001 C CNN
	1    4175 4975
	0    -1   -1   0   
$EndComp
Wire Wire Line
	4175 5125 3950 5125
$Comp
L Jumper:SolderJumper_2_Open JP27
U 1 1 60CEF4B9
P 4275 4975
F 0 "JP27" H 4275 5075 50  0001 C CNN
F 1 " " H 4275 5089 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 4275 4975 50  0001 C CNN
F 3 "~" H 4275 4975 50  0001 C CNN
	1    4275 4975
	0    -1   -1   0   
$EndComp
$Comp
L Jumper:SolderJumper_2_Open JP28
U 1 1 60CEF7CC
P 4375 4975
F 0 "JP28" H 4375 5075 50  0001 C CNN
F 1 " " H 4375 5089 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 4375 4975 50  0001 C CNN
F 3 "~" H 4375 4975 50  0001 C CNN
	1    4375 4975
	0    -1   -1   0   
$EndComp
$Comp
L Jumper:SolderJumper_2_Open JP29
U 1 1 60CEFAE4
P 4475 4975
F 0 "JP29" H 4475 5075 50  0001 C CNN
F 1 " " H 4475 5089 50  0001 C CNN
F 2 "Jumper:SolderJumper-2_P1.3mm_Open_TrianglePad1.0x1.5mm" H 4475 4975 50  0001 C CNN
F 3 "~" H 4475 4975 50  0001 C CNN
	1    4475 4975
	0    -1   -1   0   
$EndComp
Wire Wire Line
	4275 5125 4275 5225
Wire Wire Line
	4000 5225 4275 5225
Wire Wire Line
	4375 5325 4375 5125
Wire Wire Line
	4050 5325 4375 5325
Wire Wire Line
	4475 5425 4475 5125
Wire Wire Line
	4100 5425 4475 5425
Text GLabel 1725 3825 1    50   Input ~ 0
IO_CS
Text GLabel 6025 2750 0    50   Input ~ 0
UEXT_CS
$Comp
L Connector_Generic:Conn_01x03 J16
U 1 1 60ED2887
P 1325 4525
F 0 "J16" H 1375 4100 50  0000 C CNN
F 1 " " H 1375 4191 50  0000 C CNN
F 2 "Connector_PinHeader_1.27mm:PinHeader_1x03_P1.27mm_Vertical" H 1325 4525 50  0001 C CNN
F 3 "~" H 1325 4525 50  0001 C CNN
	1    1325 4525
	-1   0    0    1   
$EndComp
Wire Wire Line
	850  4125 1725 4125
Wire Wire Line
	1725 4425 1525 4425
Wire Wire Line
	1525 4525 1725 4525
Wire Wire Line
	1725 4725 1625 4725
Wire Wire Line
	1625 4725 1625 4625
Wire Wire Line
	1625 4625 1525 4625
$Comp
L Connector_Generic:Conn_01x04 J5
U 1 1 60D82C9E
P 775 900
F 0 "J5" H 855 892 50  0000 L CNN
F 1 "CS Selector" H 250 1125 50  0000 L CNN
F 2 "Connector_PinHeader_2.54mm:PinHeader_1x04_P2.54mm_Vertical" H 775 900 50  0001 C CNN
F 3 "~" H 775 900 50  0001 C CNN
	1    775  900 
	-1   0    0    -1  
$EndComp
Text GLabel 975  800  2    50   Input ~ 0
AD_CS
Text GLabel 975  1100 2    50   Input ~ 0
TFT_CS
Text GLabel 975  900  2    50   Input ~ 0
IO_CS
Text GLabel 975  1000 2    50   Input ~ 0
UEXT_CS
Text GLabel 10100 2800 2    50   Input ~ 0
Relay1
$Comp
L Connector_Generic:Conn_01x01 J18
U 1 1 611F48AB
P 5725 6175
F 0 "J18" H 5805 6217 50  0001 L CNN
F 1 " " H 5805 6171 50  0000 L CNN
F 2 "Connector_Wire:SolderWirePad_1x01_Drill0.8mm" H 5725 6175 50  0001 C CNN
F 3 "~" H 5725 6175 50  0001 C CNN
	1    5725 6175
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J20
U 1 1 611F5EE8
P 5725 6325
F 0 "J20" H 5805 6367 50  0001 L CNN
F 1 " " H 5805 6321 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 6325 50  0001 C CNN
F 3 "~" H 5725 6325 50  0001 C CNN
	1    5725 6325
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J21
U 1 1 61202D17
P 5725 6475
F 0 "J21" H 5805 6517 50  0001 L CNN
F 1 " " H 5805 6471 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 6475 50  0001 C CNN
F 3 "~" H 5725 6475 50  0001 C CNN
	1    5725 6475
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J22
U 1 1 612105D3
P 5725 6625
F 0 "J22" H 5805 6667 50  0001 L CNN
F 1 " " H 5805 6621 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 6625 50  0001 C CNN
F 3 "~" H 5725 6625 50  0001 C CNN
	1    5725 6625
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J29
U 1 1 6121E63C
P 4425 6200
F 0 "J29" H 4505 6242 50  0001 L CNN
F 1 " " H 4505 6196 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 6200 50  0001 C CNN
F 3 "~" H 4425 6200 50  0001 C CNN
	1    4425 6200
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J31
U 1 1 6121E642
P 4425 6350
F 0 "J31" H 4505 6392 50  0001 L CNN
F 1 " " H 4505 6346 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 6350 50  0001 C CNN
F 3 "~" H 4425 6350 50  0001 C CNN
	1    4425 6350
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J32
U 1 1 6121E648
P 4425 6500
F 0 "J32" H 4505 6542 50  0001 L CNN
F 1 " " H 4505 6496 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 6500 50  0001 C CNN
F 3 "~" H 4425 6500 50  0001 C CNN
	1    4425 6500
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J33
U 1 1 6121E64E
P 4425 6650
F 0 "J33" H 4505 6692 50  0001 L CNN
F 1 " " H 4505 6646 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 6650 50  0001 C CNN
F 3 "~" H 4425 6650 50  0001 C CNN
	1    4425 6650
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J34
U 1 1 6122B8F7
P 4425 7025
F 0 "J34" H 4505 7067 50  0001 L CNN
F 1 " " H 4505 7021 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 7025 50  0001 C CNN
F 3 "~" H 4425 7025 50  0001 C CNN
	1    4425 7025
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J35
U 1 1 6122B8FD
P 4425 7175
F 0 "J35" H 4505 7217 50  0001 L CNN
F 1 " " H 4505 7171 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 7175 50  0001 C CNN
F 3 "~" H 4425 7175 50  0001 C CNN
	1    4425 7175
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J36
U 1 1 6122B903
P 4425 7475
F 0 "J36" H 4505 7517 50  0001 L CNN
F 1 " " H 4505 7471 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 7475 50  0001 C CNN
F 3 "~" H 4425 7475 50  0001 C CNN
	1    4425 7475
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J37
U 1 1 6122B909
P 4425 7325
F 0 "J37" H 4505 7367 50  0001 L CNN
F 1 " " H 4505 7321 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 4425 7325 50  0001 C CNN
F 3 "~" H 4425 7325 50  0001 C CNN
	1    4425 7325
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J23
U 1 1 612389A0
P 5725 6775
F 0 "J23" H 5805 6817 50  0001 L CNN
F 1 " " H 5805 6771 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 6775 50  0001 C CNN
F 3 "~" H 5725 6775 50  0001 C CNN
	1    5725 6775
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J24
U 1 1 612389A6
P 5725 6925
F 0 "J24" H 5805 6967 50  0001 L CNN
F 1 " " H 5805 6921 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 6925 50  0001 C CNN
F 3 "~" H 5725 6925 50  0001 C CNN
	1    5725 6925
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J25
U 1 1 612389AC
P 5725 7075
F 0 "J25" H 5805 7117 50  0001 L CNN
F 1 " " H 5805 7071 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 7075 50  0001 C CNN
F 3 "~" H 5725 7075 50  0001 C CNN
	1    5725 7075
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J26
U 1 1 612389B2
P 5725 7225
F 0 "J26" H 5805 7267 50  0001 L CNN
F 1 " " H 5805 7221 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 7225 50  0001 C CNN
F 3 "~" H 5725 7225 50  0001 C CNN
	1    5725 7225
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J27
U 1 1 612457B1
P 5725 7375
F 0 "J27" H 5805 7417 50  0001 L CNN
F 1 " " H 5805 7371 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 7375 50  0001 C CNN
F 3 "~" H 5725 7375 50  0001 C CNN
	1    5725 7375
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J28
U 1 1 612457B7
P 5725 7525
F 0 "J28" H 5805 7567 50  0001 L CNN
F 1 " " H 5805 7521 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 5725 7525 50  0001 C CNN
F 3 "~" H 5725 7525 50  0001 C CNN
	1    5725 7525
	1    0    0    -1  
$EndComp
Text GLabel 4225 6500 0    50   Input ~ 0
V_MOSI
Text GLabel 4225 6200 0    50   Input ~ 0
V_CS
Text GLabel 4225 6350 0    50   Input ~ 0
V_CLK
Text GLabel 4225 6650 0    50   Input ~ 0
V_MISO
Text GLabel 4225 7175 0    50   Input ~ 0
H_CLK
Text GLabel 4225 7475 0    50   Input ~ 0
H_MISO
Text GLabel 5525 6175 0    50   Input ~ 0
SCL
Text GLabel 5525 6325 0    50   Input ~ 0
SDA
Text GLabel 5525 7375 0    50   Input ~ 0
32
Text GLabel 5525 7525 0    50   Input ~ 0
33
Text GLabel 5525 6475 0    50   Input ~ 0
TXD
Text GLabel 5525 6625 0    50   Input ~ 0
RXD
Text GLabel 4225 7325 0    50   Input ~ 0
H_MOSI
Text GLabel 4225 7025 0    50   Input ~ 0
H_CS
Text GLabel 2650 6425 0    50   Input ~ 0
34
Text GLabel 2650 6575 0    50   Input ~ 0
35
Text GLabel 2650 6275 0    50   Input ~ 0
39
Text GLabel 2650 6125 0    50   Input ~ 0
36
Wire Wire Line
	2450 6425 2650 6425
Wire Wire Line
	2450 6575 2650 6575
Text GLabel 2450 6425 0    50   Input ~ 0
BTN1
Text GLabel 2450 6575 0    50   Input ~ 0
BTN2
$Comp
L Connector_Generic:Conn_01x01 J38
U 1 1 61898B86
P 2850 6125
F 0 "J38" H 2930 6167 50  0001 L CNN
F 1 " " H 2930 6121 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 2850 6125 50  0001 C CNN
F 3 "~" H 2850 6125 50  0001 C CNN
	1    2850 6125
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J39
U 1 1 618996F5
P 2850 6275
F 0 "J39" H 2930 6317 50  0001 L CNN
F 1 " " H 2930 6271 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 2850 6275 50  0001 C CNN
F 3 "~" H 2850 6275 50  0001 C CNN
	1    2850 6275
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J40
U 1 1 618A69C3
P 2850 6425
F 0 "J40" H 2930 6467 50  0001 L CNN
F 1 " " H 2930 6421 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 2850 6425 50  0001 C CNN
F 3 "~" H 2850 6425 50  0001 C CNN
	1    2850 6425
	1    0    0    -1  
$EndComp
$Comp
L Connector_Generic:Conn_01x01 J41
U 1 1 618B3BDB
P 2850 6575
F 0 "J41" H 2930 6617 50  0001 L CNN
F 1 " " H 2930 6571 50  0000 L CNN
F 2 "RiseLogo:SolderWire_01x01_2.54" H 2850 6575 50  0001 C CNN
F 3 "~" H 2850 6575 50  0001 C CNN
	1    2850 6575
	1    0    0    -1  
$EndComp
Text GLabel 5525 7225 0    50   Input ~ 0
27
Text GLabel 5525 7075 0    50   Input ~ 0
26
Text GLabel 5525 6925 0    50   Input ~ 0
25
Text GLabel 5525 6775 0    50   Input ~ 0
04
Connection ~ 10750 1250
Connection ~ 10750 1150
$EndSCHEMATC
