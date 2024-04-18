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

    public Camera Camera;


    public GameObject[] blocks;

    
    public int arrayPos;

/*    public bool pos1;
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
                Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, -1), Camera.transform.rotation);
                hasSpawned = true;
            }
            if (input[2].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, 0), Camera.transform.rotation);
                hasSpawned = true;
            }
            if (input[3].ToString() == "1")
            {
                Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, 1), Camera.transform.rotation);
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
        if (buttonPressed == true && input == null)
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