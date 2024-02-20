#include <SD.h>
#include <RTClib.h>

class SDHelper{
    private:

    String logName;
    uint8_t pin;
    SPIClass* spiSD;

    bool FileExists(String fileName);

    void CreateFile(String fileName);

    public:

    void printDirectory(File dir, int numTabs);

    void Init();

    void printDirectory();

    void Log(DateTime time);

    void Log(String fileName, DateTime time);

    SDHelper(uint8_t sdPin, String logName);
};