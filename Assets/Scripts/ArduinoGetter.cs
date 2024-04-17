using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;

public class ArduinoGetter
{
    static string portName = "COM3"; // Adjust to your Arduino's COM port
    static int baudRate = 9600; 
    static SerialPort serialPort;

    public static char[] PhysicalBlockState; //this is the changing value!

    public static void MyThreadLoop() //"main"
    {
        OpenSerialPort();
        while (true)
        {
        //blah do hardware processing..
        //Begin "hardware processing"...
        PhysicalBlockState = ReadFromSerialPort();
        }
    }

    static void OpenSerialPort()
    {
        //Assigns Serial Port. 
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 50; // Adjust as necessary
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    static char[] ReadFromSerialPort() //could add parameter here- pass in string message from serialPort.ReadLine() 
    {
        //Gets the raw string from Serial Port. Returns a char array.
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    //debug code for lack of arduino on hand "we ball"
                    //DOESN'T WORK UNLESS SERIALPORT IS OPEN!!!!!!!! DUMBASS!!!!!!!! WHO WROTE THIS???(i am dumbass)

                    if (message != "")
                    {
                        char[] FinalArdArray = ArduinoStringToArray(message);
                        return FinalArdArray;
                    }
                    

                }
                catch (TimeoutException) { }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            return null;
    }

    static char[] ArduinoStringToArray(string ArduinoString)
    {
        //Turns the String into a char array. returns a char array. (that array is then passed up)
        string FinalString = "";
        char[] arduinoArray = ArduinoString.ToCharArray();

        Console.WriteLine("String of " + arduinoArray.Length + "numbers");

        //conversion of string 0 and 1's into the blocks
        //there needs to be a different PREFAB for each possible input!!!!!
        ///Finally we will Among
        foreach (char str in arduinoArray)
        {
            FinalString = FinalString + " " + str;
        }

        if (FinalString == "")
        {
            Console.WriteLine("Couldn't convert to array.");
            return arduinoArray;
        }
        else
        {
            Console.WriteLine("Output of array is: " + FinalString);
        }
        return arduinoArray;
    }
}


