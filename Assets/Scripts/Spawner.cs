using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;
using UnityEditor.Search;

public class Spawner : MonoBehaviour
{

    //public Camera Camera;
    public GameObject SpawnPosGuide;

    public GameObject[] blocks;

    
    public int arrayPos;

/*  public bool pos1;
    public bool pos2;
    public bool pos3;*/

    private bool hasSpawned;
    private bool buttonPressed;

    public TMP_Text blockType;


    void Update()
    {   
        CycleBlocks();
        DisplayPos();
        Spawn();
        //ModularSpawn(); //new spawn method that utilises a foreach loop, can accept as many inputs as neccessary

    }

    void DisplayPos() 
    {
        if (arrayPos == 0)
        {
            blockType.text = "Block Type: Normal";
        }

        if (arrayPos == 1)
        {
            blockType.text = "Block Type: Icy";
        }

        if (arrayPos == 2)
        {
            blockType.text = "Block Type: Bouncy";
        }
    }

    void Spawn()
    {
        char[] input = GetComponent<ArduinoReader>().OutputArray;
        if (input[0].ToString() == "1" && buttonPressed == false)
        {

            buttonPressed = true;
            if (input[1].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(4, 1, -1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[2].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(4, 1, 0), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[3].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(4, 1, 1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            //temporary hardcode 3x3 grid until foreach is working
            if (input[4].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(5, 1, -1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[5].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(5, 1, 0), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[6].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(5, 1, 1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[7].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(6, 1, -1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[8].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(6, 1, 0), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }
            if (input[9].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(6, 1, 1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, 0, 0));
                hasSpawned = true;
            }


            if (hasSpawned == true)
            {
                Debug.Log("Can't spawn just yet.");
            }

            else
            {
                Debug.Log("No blocks to spawn");
                hasSpawned = false;
            }
        }
        if (buttonPressed == true && input[0].ToString() == "0") //reset buttonpressed if no input is detected
        {
            buttonPressed = false;
        } 
        
    }

    private void ModularSpawn() //WIP: Do not use this method. 
    {
        char[] arduinoInput = GetComponent<ArduinoReader>().OutputArray;
        if (arduinoInput[0].ToString() == "1" && buttonPressed == false)
        {
            foreach (char c in arduinoInput)
            {
                if (c.ToString() == "1")
                {
                    hasSpawned = true;
                    Instantiate(blocks[arrayPos], SpawnPosGuide.transform.position + new Vector3(6, 1, 1), new Quaternion(0, SpawnPosGuide.transform.rotation.y, SpawnPosGuide.transform.rotation.z, 0));
                }
            }
        }
        else if (buttonPressed == true && arduinoInput[0].ToString() == "0") //reset buttonpressed if no input is detected
        {
            buttonPressed = false;
        }
    }


    void CycleBlocks()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            arrayPos += 1;
            Debug.Log("Changing block.");
            if (arrayPos >= blocks.Length)
            {
                //Array resets and loops
                arrayPos = 0;
                Debug.Log("Resetting array.");
            }
        }
    }
}