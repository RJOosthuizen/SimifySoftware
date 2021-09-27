String inputString = "";         // a string to hold incoming data
boolean stringComplete = false;  // whether the string is complete
String commandString = "";

boolean isConnected = false;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  
  pinMode(3, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:


  // TODO: Add model name here so settings can be fetched

  if(stringComplete)
  {
    stringComplete = false;
    getCommand();

    if(commandString.equals("TEXT"))
    {
      String text = getTextToPrint();
      
      if(text.equals("ON"))
      {
        digitalWrite(3, HIGH);
      }

      else if(text.equals("OFF"))
      {
        digitalWrite(3, LOW);
      }
    }

    inputString = "";
  } 
}

void getCommand()
{
  if(inputString.length()>0)
  {
     commandString = inputString.substring(1,5);
  }
}

String getTextToPrint()
{
  String value = inputString.substring(5,inputString.length()-2);
  return value;
}

void serialEvent() {
  while (Serial.available()) {
    // get the new byte:
    char inChar = (char)Serial.read();
    // add it to the inputString:
    inputString += inChar;
    // if the incoming character is a newline, set a flag
    // so the main loop can do something about it:
    if (inChar == '\n') {
      stringComplete = true;
    }
  }
}
