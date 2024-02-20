#include <WiFi.h>

#define BUFFER_SIZE 64
#define TIMEOUT 10000

class WifiAPHelper{
    private:

    String logFile;

    int status;

    WiFiServer* server;

    public:

    void Init();

    void CheckAndTransfer();

    void CheckAndTransfer(String fileName);

    void CheckAndTransfer(char* ptr);

    WifiAPHelper(uint16_t port);

    WifiAPHelper(uint16_t port, String logFileName);
};