using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using Unity.Collections.LowLevel.Unsafe;
using System.Runtime.Remoting.Messaging;
//using Unity.VisualScripting; Causing error. Visual scripting not found

public class ArduinoThreadArray : MonoBehaviour
{
    static string portName = "COM5"; // Adjust to your Arduino's COM port
    static int baudRate = 9600;
    static SerialPort serialPort;

    [SerializeField] private GameObject TestCube; //thing to spawn when reed switch triggered
    private static GameObject TheThing;

    /*Threading Code:
     * WIP: This script may be unstable- use with caution.
     * DELCARE a Thread that calls ReadFromSerialPort()- what other values does it need?
     * Thread needs to communicate to the rest of the code */
    private Thread arduinoThread = new Thread(new ThreadStart(ReadFromSerialPort));
    
    // Start is called before the first frame update
    void Start()
    {
        //init Arduino SerialPort
        OpenSerialPort(); //Open port before starting thread- does this pass SerialPort to the thread?

        arduinoThread.Start();
        TheThing = TestCube; //spoof to get around Static Method

        Debug.Log("I'm alive... the code has gotten past Start..."); //test if build is successful
    }

    // Update is called once per frame
    void Update()
    {
        //ReadFromSerialPort(); //This code should be called with the Thread 

        if (Input.GetKeyDown(KeyCode.R)) //debug to see if code up to update is successful
        {
            Debug.Log("Update from ArduinoScript");
        }

    }

    static void OpenSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            serialPort.ReadTimeout = 50; // Adjust as necessary
        }
        catch (Exception e)
        {
            Debug.Log("Could not open serial port: " + e.Message);
        }
    }

    static void ReadFromSerialPort() //could add parameter here- pass in string message from serialPort.ReadLine() 
    {
        Debug.Log("Read Function called- the Thread is running."); //-----DONT NEST IN LOOP It will make a HUGE LOG and your PC will EXPLODE!!!! VIOLENTLY!!!!-----
        while (true) //Just run forever bru
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    string message = serialPort.ReadLine();
                    Debug.Log(message);
                    //debug code for lack of arduino on hand "we ball"
                    //DOESN'T WORK UNLESS SERIALPORT IS OPEN!!!!!!!! DUMBASS!!!!!!!! WHO WROTE THIS???(i am dumbass)
                    if (Input.GetKeyDown("space"))
                    {
                        message = ("Reed Switch Triggered");
                    }

                    if (message != "")
                    {
                        Debug.Log("String recieved, Reading: " + message);
                        char[] ObjectToSpawn = ArduinoStringToArray(message); //where the string is converted into a array
                        foreach (char str in ObjectToSpawn)
                        {
                            SpawnThing(str.ToString()); //spawn each object that is 1 in the positions
                        }
                    }
                }
                catch (TimeoutException) { }
                catch (Exception e)
                {
                    Debug.Log("An error occurred: " + e.Message);
                    // Optionally handle other exceptions here
                }
            }
        }
    }

    static void SpawnThing(string WhatToSpawn)
    {
        //takes string ARRAY as a argument
        //String Position = what NUMBER it is!!!!!
        //is it 1?? SPAWN!!! is it 0??? Dont spawn
        ///Create a GameObject array that corresponds to grid positions- array of objects passed in should align with the gameObject array.
        ///

        Instantiate(TheThing, new Vector3(-14, 2, 40), Quaternion.identity); //Dummy GameObject- if we get this to spawn we have a successful read 
        Debug.Log("Spawned Thing");
    }

    static char[] ArduinoStringToArray(string ArduinoString)
    {
        string FinalString = "";
        char[] arduinoArray = ArduinoString.ToCharArray();

        Debug.Log("String of " + arduinoArray.Length + "numbers");

        if (arduinoArray[0].ToString() == "1") //the first string is button true/false
        {
            Debug.Log("Button Pressed: True"); //check if button is pressed
        }

        //conversion of string 0 and 1's into the blocks
        //there needs to be a different PREFAB for each possible input!!!!!
        ///Finally we will Among
        foreach (char str in arduinoArray)
        {
            FinalString = FinalString + " " + str;
        }

        if (FinalString == "")
        {
            Debug.Log("Couldn't convert to array.");
            return arduinoArray;
        }
        else
        {
            Debug.Log("Output of array is: " + FinalString);
        }
        return arduinoArray;
    }
    void OnDestroy() //ALWAYS invoke OnDestroy when using Threads- THEY DO NOT CLOSE WHEN YOU STOP THE EDITOR!!! Your PC will EXPLODE!!!! VIOLENTLY!!!!
    {
        arduinoThread.Abort();
    }
}
