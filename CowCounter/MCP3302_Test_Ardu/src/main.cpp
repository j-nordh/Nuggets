/*
  Examples Sketch for MCP3304.h
  This example reads the voltage on CH0 through CH7 of a MCP3304 13bit ADC
  The circuit:
    CH0 through CH7 from Voltage Source  (best to use a rail-rail op amp w/ voltage divider(if needed))
    DGND and AGND to Ground
    VREF and VDD to +5V (stable 5v) (i used a tl431 to maintain consistant results from 12v)
    CS/SHDN - to digital (CS pin)
    DIN - to digital (TX pin)
    DOUT - to digital (RX pin)
    CLK - to digital  (SCK pin)
    MAX3377 - level shifter from 5V to 3.3V
  Thanks to walle86 for the SPI version of the libary and example portion for MCP3304
  Also thanks to the Arduino Reference for which this library is based on.
  written 9/3/2021
  by OuterGravity
*/
//#include <MCP3304.h>
#include "MCP_ADC.h"
#include <Arduino.h>


// #define TX 13
// #define RX 12
// #define SCK 14
// #define CS 15

MCP3204 mcp;

// int reading;
// double voltage;

// MCP3304 MCP(TX, RX, SCK, CS); // create an instance using all the nessessary Pins

void setup()
{
    //MCP_ADC.selectHSPI();
    mcp.selectHSPI();
    mcp.setSPIspeed(2100000);
    mcp.begin(15);

    Serial.begin(115200);
}

void loop()
{
    unsigned long time = micros();

    int16_t val = 0;

    // for(int i = 0; i < 10000; i++){
    //     val = mcp.differentialRead(0);
    // }

    time = micros() - time;

    val = mcp.differentialRead(0);
    
    Serial.print("Val is: ");
    Serial.println(val);
    delay(1000);

}