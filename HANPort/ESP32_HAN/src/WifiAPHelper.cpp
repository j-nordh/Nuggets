#include <WifiAPHelper.h>
#include <secrets.h>
#include <SD.h>


WifiAPHelper::WifiAPHelper(uint16_t port){
    server = new WiFiServer(port);
    status = WL_IDLE_STATUS;
    logFile = "LOG.TXT";
}

WifiAPHelper::WifiAPHelper(uint16_t port, String logFileName){
    server = new WiFiServer(port);
    status = WL_IDLE_STATUS;
    logFile = logFileName;
}

void WifiAPHelper::Init(){
    char ssid[] = SECRET_SSID; // your network SSID (name)
    char pass[] = SECRET_PASS; // your network password (use for WPA, or use as key for WEP)

    // Create open network. Change this line if you want to create an WEP network:
    status = WiFi.softAP(ssid, pass);
    if (!status) {
        Serial.println("Creating access point failed");
        // don't continue
        while (true);
    }

    // print the network name (SSID);
    Serial.print("Creating access point named: ");
    Serial.println(ssid);

    IPAddress myIP = WiFi.softAPIP();
    Serial.print("AP IP address: ");
    Serial.println(myIP);

    // start the web server on port 80
    server->begin();
    //server->setTimeout();
    Serial.println("Server started");
}

//Sends a file over http to a client 
void sendFile(WiFiClient* client, char* ptr){
    // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
    // and a content-type so the client knows what's coming, then a blank line:
    client->println("HTTP/1.1 200 OK");
    client->println("Content-type:text/plain");
    client->println("Content-disposition: attachment; fileName=\"log.txt\"");
    //client->println("Content-length: 21");
    client->println();

    // the content of the HTTP response follows the header:
    
        char clientBuf[BUFFER_SIZE];
        
        
        while (client->connected())
        {
            //auto len = log.readBytes(ptr, BUFFER_SIZE);

            //client->write(log.read());
            client->write(ptr, 32);
        }
    
    
    // The HTTP response ends with another blank line:
    client->println();
}

void WifiAPHelper::CheckAndTransfer(char* ptr){
    if (status != WiFi.status())
    {
        // it has changed update the variable
        status = WiFi.status();

        if (status == WL_CONNECTED)
        {
            // a device has connected to the AP
            Serial.println("Device connected to AP");
        }
        else
        {
            // a device has disconnected from the AP, and we are back in listening mode
            Serial.println("Device disconnected from AP");
        }
    }

    WiFiClient client = server->available(); // listen for incoming clients

    if(!client){
        client.stop();
        return;
    }

    // if you get a client,
    Serial.println("new client"); // print a message out the serial port
    String currentLine = "";      // make a String to hold incoming data from the client
    //Serial.printf("Local ip:%s, Remote ip: %s, Remote port: %i \n", client.localIP().toString().c_str(), client.remoteIP().toString().c_str(), client.remotePort());

    auto startTime = millis();

    while (client.connected())
    { // loop while the client's connected

        //Serial.println("Client connected");

        if (client.available())
        {            
            //Serial.println("Client Available");
            
                           // if there's bytes to read from the client,
            char c = client.read(); // read a byte, then
            Serial.write(c);        // print it out the serial monitor

            if (c == '\n')
            { // if the byte is a newline character

                // if the current line is blank, you got two newline characters in a row.
                // that's the end of the client HTTP request, so send a response:
                if (currentLine.length() == 0)
                {
                    //sendFile
                    Serial.println("Trying to send file");
                    sendFile(&client, ptr);

                    // break out of the while loop:
                    break;
                }
                else
                { // if you got a newline, then clear currentLine:
                    currentLine = "";
                }
            }
            else if (c != '\r')
            {                     // if you got anything else but a carriage return character,
                currentLine += c; // add it to the end of the currentLine
            }
        }

        //Remove faulty customer
        // Serial.println("Removing faulty customer");
        // break;
        if((millis()-startTime) > TIMEOUT){
            Serial.println("Removing faulty customer");
            break;
        }
    }
    
    // close the connection:
    delay(1); // Trying 
    client.stop();
    delay(1);
    Serial.println("client disconnected");
}

void WifiAPHelper::CheckAndTransfer(){
    CheckAndTransfer(logFile);
}