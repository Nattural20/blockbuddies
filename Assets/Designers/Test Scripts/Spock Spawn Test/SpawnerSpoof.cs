using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;
using UnityEditor.Search;
using System.Linq;

public class SpawnerSpoof : MonoBehaviour
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
        int[] input = GetComponent<ArduinoReaderSpoof>().OutputArray;
        if (Input.GetKeyDown(KeyCode.F))
        {
            var spockDaddy = new GameObject("spocks");
            spockDaddy.transform.position = SpawnPosGuide.transform.position;
            spockDaddy.transform.rotation = SpawnPosGuide.transform.rotation;
            var daddyList = spockDaddy.AddComponent<SpockScript>();
            daddyList.spockLayout = new int[input.Length - 1];
            var pos = 1;
            
            while (pos < input.Length)
            {
                daddyList.spockLayout[pos - 1] = input[pos];
                pos++;
            }

            if (input[1].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(0, 1, 1);
                hasSpawned = true;
            }
            if (input[2].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(0, 1, 0);
                hasSpawned = true;
            }
            if (input[3].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(0, 1, -1);
                hasSpawned = true;
            }
            //temporary hardcode 3x3 grid until foreach is working
            if (input[4].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(1, 1, 1);
                hasSpawned = true;
            }
            if (input[5].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(1, 1, 0);
                hasSpawned = true;
            }
            if (input[6].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(1, 1, -1);
                hasSpawned = true;
            }
            if (input[7].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(2, 1, 1);
                hasSpawned = true;
            }
            if (input[8].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(2, 1, 0);
                hasSpawned = true;
            }
            if (input[9].ToString() == "1")
            {
                var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
                newSpock.transform.localPosition = new Vector3(2, 1, -1);
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