#include <Arduino.h>
#include <stdio.h>
#include <stdlib.h>
#include <WiFi.h>
#include <PubSubClient.h>

// Replace with your network credentials
#define ssid "mqtt"
#define password "risemqtt"

#define RXD2 16
#define TXD2 17


const char* mqtt_server = "10.42.0.1";

WiFiClient espClient;
PubSubClient client(espClient);

// const int numChars = 32;
const int numChars = 1024;
char receivedChars[numChars];
boolean newData = false;


void setup_wifi() {

  delay(10);
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    //Serial.print(".");
    Serial.println(WiFi.status());
  }

  randomSeed(micros());

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void reconnect() {
  // Loop until we're reconnected
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    // Create a random client ID
    String clientId = "ESP8266Client-";
    clientId += String(random(0xffff), HEX);
    // Attempt to connect
    if (client.connect(clientId.c_str())) {
      Serial.println("connected");
      // Once connected, publish an announcement...
      //client.publish("outTopic", "hello world");
      // ... and resubscribe
      //client.subscribe("inTopic");
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();

  // Switch on the LED if an 1 was received as first character
  if ((char)payload[0] == '1') {
    // but actually the LED is on; this is because
    // it is active low on the ESP-01)
    Serial.print("ok");
  }

}

void readSerial()
{
    static int ndx = 0;
    char endMarker = '\n';
    endMarker = '!';
    char rc;

    while (Serial2.available() > 0 && newData == false)
    {
        rc = Serial2.read();

        if (rc != endMarker)
        {
            receivedChars[ndx] = rc;
            ndx++;
            if (ndx >= numChars)
            {
                newData = true;
                break;
                
                //ndx = numChars - 1;
            }
        }
        else
        {
            receivedChars[ndx] = '\0';
            ndx = 0;
            newData = true;
        }
    }
}

void showNewData()
{
    if (newData == true)
    {
        //Serial.print("This just in ... ");
        //Serial.println(receivedChars);
        newData = false;
        delay(10);
        client.publish("inTopic", receivedChars);
        
    }
}

void setup()
{
    // put your setup code here, to run once:

    Serial.begin(115200);

    Serial2.begin(115200, SERIAL_8N1, RXD2, TXD2);

    while (!Serial && !Serial2)
    {
        ; // Waiting for serial port(s) to start
    }

    setup_wifi();

    client.setServer(mqtt_server, 1883);
    client.setCallback(callback);

    delay(10);

    Serial.println("Controller initiated");

    
}

void loop()
{
    if (!client.connected()) {
        reconnect();
    }
    client.loop();
    // put your main code here, to run repeatedly:
    readSerial();
    showNewData();
}