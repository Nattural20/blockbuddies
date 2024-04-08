using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;

public class ArduinoSpawnObject : MonoBehaviour
{
    static string portName = "COM3"; // Adjust to your Arduino's COM port
    static int baudRate = 9600;
    static SerialPort serialPort;

    [SerializeField] private GameObject TestCube; //thing to spawn when reed switch triggered
    private static GameObject TheThing;

    // Start is called before the first frame update
    void Start()
    {
        OpenSerialPort();

        TheThing = TestCube; //spoof

        Debug.Log("I'm alive... the code has gotten past Start..."); //test if build is successful
    }

    // Update is called once per frame
    void Update()
    {
        ReadFromSerialPort(); //if read is successful from here, SpawnThing()
        Thread.Sleep(50); // Adjust as necessary

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

    static void ReadFromSerialPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string message = serialPort.ReadLine();
                Debug.Log(message);

                //debug code for lack of arduino on hand "we ball"
                if (Input.GetKeyDown("space"))
                {
                    message = ("Reed Switch Triggered");
                }

                if (message.Contains("Reed Switch Triggered"))
                {
                    Debug.Log("Reed Switch Triggered");
                    // Trigger your action for the reed switch here
                    SpawnThing(); //if message read from the port is this, SpawnThing()
                }
                else if (message.Contains("Button Pressed"))
                {
                    Debug.Log("Button Pressed");
                    // Trigger your action for the button here
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

    static void SpawnThing()
    {
        Instantiate(TheThing, new Vector3(-14, 2, 40), Quaternion.identity); //Dummy GameObject- if we get this to spawn we have a successful read 
        Debug.Log("Spawned Thing");
    }
}
