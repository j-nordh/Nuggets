#include <FolderHelper.h>

SDHelper::SDHelper(uint8_t sdPin, String logName)
{
    this->logName = logName;
    pin = sdPin;
    this->spiSD = new SPIClass(VSPI);
}

void SDHelper::Init()
{
    Serial.print("Initializing SD card..."); 
    //spiSD.begin(14, 12, 13, pin);

    if (!SD.begin(pin, *spiSD))
    {

        Serial.println("SD card initialization failed.");

        while (true)
            ;
    }
    Serial.println("initialization done.");
}

void SDHelper::Log(DateTime time)
{
    Log(logName, time);
}

void SDHelper::Log(String fileName, DateTime time)
{
    File log;

    if(!SD.exists(fileName)){
        Serial.print("File missing: ");
        Serial.println(fileName);
        Serial.println("Creating new...");
        log = SD.open(fileName, FILE_WRITE);
        log.close();
    }
    
    log = SD.open(fileName, FILE_APPEND);

    char sample[50];
    sprintf(sample, "%04d-%02d-%02d %02d:%02d:%02d", time.year(), time.month(), time.day(), time.hour(), time.minute(), time.second());
    //sprintf(sample, "T:%04d-%02d-%02dT%02d:%02d:%02d TV:%s LV:%ld", time.year(), time.month(), time.day(), time.hour(), time.minute(), time.second(), tempString.c_str(), lightValue);
    
    log.println(sample);
    Serial.println(sample);

    log.close();
}

void SDHelper::printDirectory(File dir, int numTabs)
{
    while (true)
    {
        File entry = dir.openNextFile();

        if (!entry)
        {
            // no more files
            break;
        }

        for (uint8_t i = 0; i < numTabs; i++)
        {
            Serial.print('\t');
        }

        Serial.print(entry.name());

        if (entry.isDirectory())
        {
            Serial.println("/");
            printDirectory(entry, numTabs + 1);
        }
        else
        {
            // files have sizes, directories do not
            Serial.print("\t\t");
            Serial.println(entry.size(), DEC);
        }
        entry.close();
    }
}
