#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>
#include <unistd.h>
#include <time.h>
#include <sys/stat.h>

#include <inttypes.h>

#include <pigpio.h>

#define USE_GPIO

// #define PIN_NUMBER 3
// #define INTERRUPT_NUMBER 2

#define PEAK_PIN 22
#define TRIG_PIN 27

#define PEAK_LED 14
#define TRIG_LED 15

#define LOG_LED 18



#define INTERRUPT_TIMEOUT 0

//#define FILE_NAME "/home/pi/CowCounter/test.csv"
#define FILE_NAME "/media/usb0/cowcount.csv"

/// Convert seconds to nanoseconds
#define SEC_TO_NS(sec) ((sec)*1000000000)

FILE *fpt;

// uint64_t now, past, diff;

#define LED_DELAY 500000000 // 500ms in ns

#define SLEEP_DELAY 100000

//#define MARK_DELAY 200000000
#define MARK_DELAY LED_DELAY

struct timespec ts, trig_ts, peak_ts, trig_real_ts;

int trigOn, peakOn, peakLed, trigLed, logLed = 0;

uint64_t specToNs(struct timespec* timesp){
    return SEC_TO_NS((uint64_t)timesp->tv_sec) + (uint64_t)timesp->tv_nsec;
}

uint64_t checkTime(struct timespec* pastTs)
{
    uint64_t past = specToNs(pastTs);
    
    clock_gettime(CLOCK_MONOTONIC, &ts);

    uint64_t now = SEC_TO_NS((uint64_t)ts.tv_sec) + (uint64_t)ts.tv_nsec;
    return now - past;
}

// void alertFunc(int gpio, int level, uint32_t tick)
// {

//     printf("Alert: %i \n", level);

//     diff = checkTime();

//     printf("%" PRIu64 "\n", diff);
// }

void interruptFunc(int gpio, int level, uint32_t tick)
{
    printf("Interrupt on gpio %i w/ level %i \n", gpio, level);

    if (gpio == PEAK_PIN && level == 0)
    {
        peakOn = 1;
        if(!peakLed){
            clock_gettime(CLOCK_MONOTONIC, &peak_ts);
            peakLed = 1;
            gpioWrite(PEAK_LED, 1);
        }

    }
    else if (gpio == TRIG_PIN && level == 0)
    {
        if(trigOn)
            return;

        trigOn = 1;
        trigLed = 1;
        clock_gettime(CLOCK_MONOTONIC, &trig_ts);
        clock_gettime(CLOCK_REALTIME, &trig_real_ts);
        gpioWrite(TRIG_LED, 1);
    }
}

int checkIfFileExists(const char *filename)
{
    struct stat buffer;
    int exist = stat(filename, &buffer);
    if (exist == 0)
        return 1;
    else
        return 0;
}

void logCow()
{

    logLed = !logLed;

    gpioWrite(LOG_LED, logLed);

    //struct timespec logts;
    //clock_gettime(CLOCK_REALTIME, &logts);
    // time_t now;
    // time(&now);

    char buf[sizeof "2011-10-08T07:07:09Z"]; //Example string for timestamp
    strftime(buf, sizeof buf, "%FT%TZ", gmtime(&trig_real_ts.tv_sec));
    printf("%s.%ld\n", buf, trig_real_ts.tv_nsec);
    fprintf(fpt, "%s.%ld\n", buf, trig_real_ts.tv_nsec);
    fflush(fpt);
}

int main(int argc, char *argv[])
{

    printf("Started counting cows \n");

    int fileExists = checkIfFileExists(FILE_NAME);

    fpt = fopen(FILE_NAME, "a+");

    if (!fileExists)
        fprintf(fpt, "Timestamp\n");

    clock_gettime(CLOCK_REALTIME, &ts);

    gpioInitialise();

    gpioSetMode(PEAK_PIN, PI_INPUT);
    gpioSetMode(TRIG_PIN, PI_INPUT);
    //gpioSetPullUpDown(PEAK_PIN, PI_PUD_UP);
    //gpioSetPullUpDown(TRIG_PIN, PI_PUD_UP);
    

    gpioSetMode(LOG_LED, PI_OUTPUT);
    gpioSetMode(PEAK_LED, PI_OUTPUT);
    gpioSetMode(TRIG_LED, PI_OUTPUT);


    //gpioSetAlertFunc(INTERRUPT_NUMBER, &interruptFunc);
    gpioSetISRFunc(PEAK_PIN, FALLING_EDGE, INTERRUPT_TIMEOUT, &interruptFunc);
    gpioSetISRFunc(TRIG_PIN, FALLING_EDGE, INTERRUPT_TIMEOUT, &interruptFunc);

    // atexit(test);

    while (1)
    {
        // clock_gettime(CLOCK_REALTIME, &trig_real_ts);
        // logCow();

        // sleep(10);

        if (trigOn && (checkTime(&trig_ts) > MARK_DELAY))
        {
            if(!peakOn)
                logCow();

            trigOn = 0;
            peakOn = 0;
        }

        if(trigLed && (checkTime(&trig_ts) > LED_DELAY)){
            trigLed = 0;
            gpioWrite(TRIG_LED, 0);
        }

        if(peakLed && (checkTime(&peak_ts) > LED_DELAY)){
            peakLed = 0;
            gpioWrite(PEAK_LED, 0);
        }

        // pause();
        // sigsuspend();
        usleep(100000);
    }

    gpioTerminate();
}