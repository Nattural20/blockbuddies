using System.Collections;
using System.IO.Ports;
using System;
using System.Threading;

public class ArduinoGetterV2
{
    public enum Orientation
    {
        Left,
        Up,
        Right,
        Down
    }

    static string portName; // Set this to your Arduino's COM port
    static int baudRate = 9600;
    static SerialPort serialPort;
    public static char[] PhysicalBlockState; // The changing value

    public static Orientation selectedOrientation = Orientation.Left;
    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public static void StartArduinoThread()
    {
        Thread arduinoThread = new Thread(() => MyThreadLoop(cancellationTokenSource.Token));
        arduinoThread.Start();
        Console.WriteLine("Arduino thread started.");
    }

    public static void StopArduinoThread()
    {
        cancellationTokenSource.Cancel();
        Console.WriteLine("Arduino thread stopped.");
    }

    public static void MyThreadLoop(CancellationToken cancellationToken)
    {
        ArduinoListener listener = new ArduinoListener();
        portName = listener.FindComPort();
        OpenSerialPort();

        while (!cancellationToken.IsCancellationRequested)
        {
            PhysicalBlockState = ReadFromSerialPort();

            // Sleep to prevent busy-waiting
            Thread.Sleep(50);
        }

        CloseSerialPort();
    }

    static void OpenSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 50; // Adjust as necessary
            serialPort.DtrEnable = true;
            Console.WriteLine("Serial port opened.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error opening serial port: {e}");
        }
    }

    static char[] ReadFromSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string message = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(message))
                {
                    return ArduinoStringToArray(message, selectedOrientation);
                }
            }
            catch (TimeoutException) { }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading from serial port: {e}");
            }
        }
        return new char[0]; // Return an empty array if no valid data is read
    }

    static void CloseSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Console.WriteLine("Serial port closed.");
        }
    }

    static char[] ArduinoStringToArray(string ArduinoString, Orientation orientation)
    {
        char[] arduinoArray = ArduinoString.ToCharArray();
        char[] rotatedArray = RotateGriddy(arduinoArray, orientation);
        Console.WriteLine($"Received array: {new string(rotatedArray)}");
        return rotatedArray;
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
                // Rotate logic for 'Up'
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
                // Rotate logic for 'Right'
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
                // Rotate logic for 'Down'
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

    public static void SetOrientation(Orientation newOrientation)
    {
        selectedOrientation = newOrientation;
    }
}
