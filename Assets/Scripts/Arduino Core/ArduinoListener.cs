using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.ProBuilder.MeshOperations;

public class ArduinoListener
{
    public string comPortReal;
    public string[] _possibleComPorts = {"COM2",  "COM3", "COM4", "COM5", "COM6"};
    static SerialPort serialPort;
    static int baudRate = 9600;
    public string TEST_COM_Port = "COM4" ;

    /// <summary>
    /// WIP Arduino Listener. 
    /// </summary>

    //public string FindComPort()
    //{///Family Guy Funny Moment Compilations #23
    //    //go through and try every port, see what returns something
    //    foreach (string port in _possibleComPorts)
    //    {
    //        serialPort = new SerialPort(port, baudRate);
    //        try
    //        {
    //            serialPort.Open();
    //            serialPort.ReadTimeout = 50; // Adjust as necessary
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine(e);
    //            return ("No Port Found on: " + port);
    //        }
    //        if (serialPort != null)
    //        {
    //            comPortReal = port;
    //            return port;
    //        }
    //    }
    //    return ("Reached end of method. No Port Found.");
    //}

    public string FindComPort()
    {
        foreach (string port in _possibleComPorts)
        {
            serialPort = new SerialPort(port, baudRate);
            try
            {
                serialPort.Open();
                serialPort.ReadTimeout = 50; // Adjust as necessary

                comPortReal = port;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error on port " + port + ": " + e.Message);
            }
            finally
            {
                // Ensure the port is closed if it was successfully opened
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
        }
        if (comPortReal == null)
        {
            comPortReal = ("Port error- could not find port.");
        }
        return comPortReal;
    }
    public string ReturnTestPort()
    {
        return TEST_COM_Port;
    }
}
