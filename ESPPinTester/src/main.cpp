#include <Arduino.h>

#include <ETH.h>
#include <ESPAsyncWebServer.h>
#include <ArduinoOTA.h>
#include <ESP32Ping.h>
#include <HTTPClient.h>

#define TESTPIN 2 
int pins[] = {35};
//int pins[] = {0, 1, 2, 3, 4, 5, 13, 14, 15, 16, 32, 34, 35, 36, 39};
//int pins[] = {13, 14, 15, 16, 32, 33, 34, 35, 36, 39};

bool connected = false;
IPAddress gateway = IPAddress(192, 198, 1, 1);
HTTPClient http;
void WiFiEvent(WiFiEvent_t event)
{
  switch (event)
  {
  case SYSTEM_EVENT_ETH_START:
    Serial.println("ETH Started");
    //set eth hostname here
    ETH.setHostname("PinTester");
    break;
  case SYSTEM_EVENT_ETH_CONNECTED:
    Serial.println("ETH Connected");
    break;
  case SYSTEM_EVENT_ETH_GOT_IP:
    Serial.print("ETH MAC: ");
    Serial.print(ETH.macAddress());
    Serial.print(", IPv4: ");
    Serial.print(ETH.localIP());
    if (ETH.fullDuplex())
    {
      Serial.print(", FULL_DUPLEX");
    }
    Serial.print(", ");
    Serial.print(ETH.linkSpeed());
    Serial.println("Mbps");
    if (connected)
      return;
    connected = true;
    ArduinoOTA.begin();

    break;
  case SYSTEM_EVENT_ETH_DISCONNECTED:
    Serial.println("ETH Disconnected");
    connected = false;
    break;
  case SYSTEM_EVENT_ETH_STOP:
    Serial.println("ETH Stopped");
    connected = false;
    break;
  default:
    break;
  }
}
void initOTA()
{
  ArduinoOTA
      .onStart([]()
               {
                 String type;
                 if (ArduinoOTA.getCommand() == U_FLASH)
                   type = "sketch";
                 else // U_SPIFFS
                   type = "filesystem";

                 // NOTE: if updating SPIFFS this would be the place to unmount SPIFFS using SPIFFS.end()
                 Serial.println("Start updating " + type);
               })
      .onEnd([]()
             { Serial.println("\nEnd"); })
      .onProgress([](unsigned int progress, unsigned int total)
                  { Serial.printf("Progress: %u%%\r", (progress / (total / 100))); })
      .onError([](ota_error_t error)
               {
                 Serial.printf("Error[%u]: ", error);
                 if (error == OTA_AUTH_ERROR)
                   Serial.println("Auth Failed");
                 else if (error == OTA_BEGIN_ERROR)
                   Serial.println("Begin Failed");
                 else if (error == OTA_CONNECT_ERROR)
                   Serial.println("Connect Failed");
                 else if (error == OTA_RECEIVE_ERROR)
                   Serial.println("Receive Failed");
                 else if (error == OTA_END_ERROR)
                   Serial.println("End Failed");
               });
  Serial.println("OTA initialized");
  Serial.println("Setup complete!");
}

void initNetworking()
{
  WiFi.onEvent(WiFiEvent);
  initOTA();
  ETH.begin();
  ETH.config(IPAddress(192, 168, 1, 186), gateway, IPAddress(255, 255, 255, 0), IPAddress(8, 8, 8, 8), IPAddress(0, 0, 0, 0));
  Serial.print("Waiting for IP-address: ");
  while(!connected)
  {
    Serial.print(".");
    delay(500);
  }
  Serial.println("OK");
}

void setup()
{
  Serial.begin(115200);
  while (!Serial)
  {
    yield();
    delay(50);
  }
  initNetworking();
}
void wait()
{
  while (true)
    if (Serial.read() == '\n')
      break;
}
bool testFunctions()
{
  http.begin("http://192.168.1.1/");

  Serial.print("    Contacting gateway: "); 
  // Send HTTP GET request
  int httpResponseCode = http.GET();
  bool get =false;
  if (httpResponseCode>0) {
    Serial.println("OK");
    //Serial.println(http.getString());
    get = true;
  }
  else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  // Free resources
  http.end();
  // Serial.print("Pinging gateway: ");
  // bool ping = Ping.ping(gateway, 1);
  // Serial.println(ping ? "Success" : "Failed");
  return get;
}
void writeAndTest(uint8_t pin, uint8_t val, bool &readback, bool &test)
{
  digitalWrite(pin, val);
  delay(10);
  pinMode(TESTPIN, INPUT);
  uint8_t res = digitalRead(TESTPIN);
  readback = res == val;
  Serial.print("    Read: ");
  Serial.println(readback? "OK" : "Failed");
  test = testFunctions();
  Serial.print("    Function: ");
  Serial.println(test ? "OK    " : "Failed");
  
}

void readAndTest(uint8_t pin, uint8_t val, bool &read, bool &test)
{
  pinMode(TESTPIN, OUTPUT);
  digitalWrite(TESTPIN, val);
  delay(10);
  read = digitalRead(pin) == val;
  Serial.print("    Read: ");
  Serial.println(read? "OK" : "Failed");
  test = testFunctions();
  Serial.print("    Function: ");
  Serial.println(test ? "OK    " : "Failed");
}

void loop()
{
  Serial.println("Headers:");
  Serial.println("Pin\tInit\tWrite0\tWrite0Test\tWrite1\tWrite1Test\tPullupRead\tPullupTest\tPulldownRead\tPulldownTest\tGndRead\tGndTest\tVccRead\tVccTest");
  int count = sizeof(pins) / sizeof( * pins);
  Serial.println("Running pre-test:");
  testFunctions();
  Serial.printf("Will evaluate %d pins.\n", count);
  for (int i = 0; i < count; i++)
  {
    auto pin = pins[i];
    Serial.printf("Preparing to test pin %d (index %d).", pin, i);

    bool initTest, write0, write0Test, write1, write1Test;
    bool pullupRead, pullupTest;
    bool pulldownRead, pulldownTest;
    bool gndRead, gndTest;
    bool vccRead, vccTest;
    initTest = testFunctions();
    Serial.println("Testing inputs");
    Serial.println("  Pullup:");
    pinMode(pin, INPUT_PULLUP);
    readAndTest(pin, 1, pullupRead, pullupTest);

    Serial.println("  Pulldown:");
    pinMode(pin, INPUT_PULLDOWN);
    readAndTest(pin, 0, pulldownRead, pulldownTest);
    Serial.println("Please connect the test wire. Press enter when finished.");
    wait();
    pinMode(pin, INPUT);
    Serial.println("  No pull, GND:");
    readAndTest(pin, 0, gndRead, gndTest);
    Serial.println("  No pull, 3V3:");
    readAndTest(pin, 1, vccRead, vccTest);
    
    Serial.println("Testing output:");
    Serial.println("  0");
    pinMode(pin, OUTPUT);
    writeAndTest(pin, 0, write0, write0Test);
    Serial.println("  1");
    writeAndTest(pin, 1, write1, write1Test);

    pinMode(pin, INPUT);
    Serial.printf("\n\n%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\t%d\n\n",
                  pin, initTest, 
                  write0, write0Test, 
                  write1, write1Test,
                  pullupRead, pullupTest,
                  pulldownRead, pulldownTest,
                  gndRead, gndTest,
                  vccRead, vccTest);
  }
  // put your main code here, to run repeatedly:
}
