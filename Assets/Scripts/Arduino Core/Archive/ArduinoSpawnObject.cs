using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO.Ports;
using UnityEngine;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

public class ArduinoSpawnObject : MonoBehaviour
{
    static string portName = "COM5"; // Adjust to your Arduino's COM port
    static int baudRate = 9600;
    static SerialPort serialPort;

    [SerializeField] private GameObject TestCube; //thing to spawn when reed switch triggered
    private static GameObject TheThing;
    static private bool hasSpawned = false; //check if object has been spawned by the input and dont spawn another

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

        //debug code, doesn't work without a port open
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SpawnThing(); //Spawn single object
        }

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

                //debug code, doesn't work without a port open
                if (Input.GetKeyDown(KeyCode.E))
                {
                    message = ("0100");
                }

                if ((message != "0000") && (hasSpawned == false)) //if message is not 0 inputs AND nothing has been spawned
                {
                    Debug.Log("Spawned Object");
                    SpawnThing(); //Spawn single object
                    hasSpawned = true;
                }
                else if (message == "0000") //if nothing is input, reset bool to spawn another one
                {
                    hasSpawned = false;
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
        Instantiate(TheThing, new Vector3(1.06f, 1f, 11.29f), Quaternion.identity); //Dummy GameObject- if we get this to spawn we have a successful read 
        Debug.Log("Spawned Thing");
    }
}
