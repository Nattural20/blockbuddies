using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using System.Drawing.Printing;

public class ArduinoGetter
{
    public enum Orientation
    {
        Left,
        Up,
        Right,
        Down
    }


    static string portName; // Adjust to your Arduino's COM port, uses the listener script to auto find open comport
    static int baudRate = 9600; 
    static SerialPort serialPort;

    public static char[] PhysicalBlockState; //this is the changing value!

    public static Orientation selectedOrientation = Orientation.Left;
    

    public static void MyThreadLoop() //"main"
    {
        ArduinoListener listener = new ArduinoListener();
        portName = listener.FindComPort();
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
            serialPort.DtrEnable = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    static char[] ReadFromSerialPort() 
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
                    char[] FinalArdArray = ArduinoStringToArray(message, selectedOrientation);
                    return FinalArdArray;
                }
                else {
                    //previous
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

    static char[] ArduinoStringToArray(string ArduinoString, Orientation orientation)
    {
        //Turns the String into a char array. returns a char array. (that array is then passed up)
        
        char[] arduinoArray = ArduinoString.ToCharArray();
        char[] rotatedArray = RotateGriddy(arduinoArray, orientation);


        string FinalString = new string(rotatedArray);
        Console.WriteLine("String of " + arduinoArray.Length + "numbers");

        return rotatedArray;

        //conversion of string 0 and 1's into the blocks
        //there needs to be a different PREFAB for each possible input!!!!!
        ///Finally we will Among
/*        foreach (char str in arduinoArray)
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
        return arduinoArray;*/
    }

    public static void SetOrientation(Orientation newOrientation)
    {
        selectedOrientation = newOrientation;
    }

    static char[] RotateGriddy(char[] array, Orientation orientation)
    {
        char[] rotatedArray = new char[10];

        switch (orientation)
        {
            case Orientation.Left:
                rotatedArray = array;
                break;
            case Orientation.Up:
                rotatedArray[1] = array[7];
                rotatedArray[2] = array[4];
                rotatedArray[3] = array[1];
                rotatedArray[4] = array[8];
                rotatedArray[5] = array[5];
                rotatedArray[6] = array[2];
                rotatedArray[7] = array[9];
                rotatedArray[8] = array[6];
                rotatedArray[9] = array[3];
                break;
            case Orientation.Right:
                rotatedArray[1] = array[9];
                rotatedArray[2] = array[8];
                rotatedArray[3] = array[7];
                rotatedArray[4] = array[6];
                rotatedArray[5] = array[5];
                rotatedArray[6] = array[4];
                rotatedArray[7] = array[3];
                rotatedArray[8] = array[2];
                rotatedArray[9] = array[1];
                break;
            case Orientation.Down:
                rotatedArray[1] = array[3];
                rotatedArray[2] = array[6];
                rotatedArray[3] = array[9];
                rotatedArray[4] = array[2];
                rotatedArray[5] = array[5];
                rotatedArray[6] = array[8];
                rotatedArray[7] = array[1];
                rotatedArray[8] = array[4];
                rotatedArray[9] = array[7];
                break;
        }

        return rotatedArray;
    }


}


