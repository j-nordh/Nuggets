#define SECRET_SSID "Hej_Johannes"
#define SECRET_PASS "password123"



        // if (c == '\n') {                    // if the byte is a newline character

        //   // if the current line is blank, you got two newline characters in a row.
        //   // that's the end of the client HTTP request, so send a response:
        //   if (currentLine.length() == 0) {
        //     // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
        //     // and a content-type so the client knows what's coming, then a blank line:
        //     client.println("HTTP/1.1 200 OK");
        //     client.println("Content-type:text/html");
        //     client.println();

        //     // the content of the HTTP response follows the header:
        //     client.print("Click <a href=\"/H\">here</a> turn the LED on<br>");
        //     client.print("Click <a href=\"/L\">here</a> turn the LED off<br>");
        //     client.print("Click here to <a href=\"/Test\" download>download</a> <br>");

        //     // The HTTP response ends with another blank line:
        //     client.println();
        //     // break out of the while loop:
        //     break;
        //   }
        //   else {      // if you got a newline, then clear currentLine:
        //     currentLine = "";
        //   }
        // }
        // else if (c != '\r') {    // if you got anything else but a carriage return character,
        //   currentLine += c;      // add it to the end of the currentLine
        // }

        // // Check to see if the client request was "GET /H" or "GET /L":
        // if (currentLine.endsWith("GET /H")) {
        //   //digitalWrite(led, HIGH);               // GET /H turns the LED on
        // }
        // if (currentLine.endsWith("GET /L")) {
        //   //digitalWrite(led, LOW);                // GET /L turns the LED off
        // }
        // if(currentLine.endsWith("GET /Test")){
        //   //Läs SD-kort
        //   client.println("HTTP/1.1 200 OK"); //send new page
        //   client.println("Content-Type: text/text");
        //   client.println();
        //   client.println("Test, test test :-D");
        //   client.println();
        //   break;
        // }