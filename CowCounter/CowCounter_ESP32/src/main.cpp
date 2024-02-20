#include <Arduino.h>
#include <SPI.h>
#include <Wire.h>
#include <SD.h>
#include <RTClib.h>

#include "FolderHelper.h"

#define SD_PIN 05
#define FILE_NAME "/log.csv"

// GPIO 21 (SDA)
// GPIO 22 (SCL)
// VSPI

#define PEAK_PIN 36
#define TRIG_PIN 39

#define PEAK_LED 27
#define TRIG_LED 25
#define LOG_LED 26

// #define LED_DELAY 500000000 // 500ms in ns
#define LED_DELAY 50 // in ms

// #define SLEEP_DELAY 100000 // in us
#define SLEEP_DELAY 50 // in ms

#define PULSE_WINDOW 250

unsigned long trig_ts, peak_ts, log_ts;

int trigOn, peakOn, peakLed, trigLed, logLed = 0;

// RTC library
RTC_PCF8523 rtc;

SDHelper sdHelper(SD_PIN, FILE_NAME);

void peakInterrupt()
{
    if (peakOn)
        return;

    peakOn = 1;
    peakLed = 1;
    peak_ts = millis();
    digitalWrite(PEAK_LED, peakLed);
    Serial.print("-");
}

void trigInterupt()
{
    if (trigOn)
        return;

    trigOn = 1;
    trigLed = 1;
    trig_ts = millis();
    digitalWrite(TRIG_LED, trigLed);
    Serial.print("+");
}

void logCow()
{
    // logLed = !logLed;
    log_ts = millis();
    logLed = 1;
    digitalWrite(LOG_LED, logLed);
    sdHelper.Log(rtc.now());
}

void setup()
{
    
    Serial.begin(115200);

    rtc.begin();
    Serial.printf("Initializing Clock... \n");
    
    //Uncomment to adjust
    //rtc.adjust(DateTime(F(__DATE__), F(__TIME__)));

    Serial.print("Clock started, Time is: ");
    
    Serial.println(rtc.now().timestamp());

    sdHelper.Init();
    Serial.printf("Started SD card \n");

    // Set leds to output
    pinMode(LOG_LED, OUTPUT);
    pinMode(PEAK_LED, OUTPUT);
    pinMode(TRIG_LED, OUTPUT);

    // Set interrupt pins to input
    pinMode(PEAK_PIN, INPUT);
    pinMode(TRIG_PIN, INPUT);

    attachInterrupt(PEAK_PIN, &peakInterrupt, FALLING);
    attachInterrupt(TRIG_PIN, &trigInterupt, FALLING);

    Serial.printf("Started counting cows \n");
}

void loop()
{
    unsigned long current = millis();

    if (trigOn && (current - trig_ts > PULSE_WINDOW))
    {
        if (!peakOn)
            logCow();

        trigOn = 0;
        peakOn = 0;
    }

    // Handle leds
    if (trigLed && (current - trig_ts > LED_DELAY))
    {
        trigLed = 0;
        peakLed = 0;
        digitalWrite(TRIG_LED, trigLed);
        digitalWrite(PEAK_LED, peakLed);
    }

    if (logLed && (current - log_ts > (LED_DELAY)))
    {
        logLed = 0;
        digitalWrite(LOG_LED, logLed);
    }

    delay(SLEEP_DELAY);
}