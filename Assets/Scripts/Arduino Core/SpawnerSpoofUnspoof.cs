using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;

public class SpawnerSpoofUnspoof : MonoBehaviour
{

    //public Camera Camera;
    public GameObject SpawnPosGuide, spockShell;

    public GameObject[] blocks, ghostSpocks;

    
    public int arrayPos;

/*  public bool pos1;
    public bool pos2;
    public bool pos3;*/

    public bool hasSpawned;
    public bool buttonPressed;
    public bool canSpawnSpocks;

    public TMP_Text blockType;

    // Change in griddy detector
    public char[] input;
    public char[] previousInput;

    bool hasErrored = false;

    void Update()
    {   
        CycleBlocks();
        //DisplayPos();
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
        input = GetComponent<ArduinoReader>().OutputArray;

        if (input != null)
        {
            if (canSpawnSpocks)
            {
                UpdateGhostSpocks(input);
            }

            if (input[0].ToString() == "1" && buttonPressed == false)
            {
                buttonPressed = true;

                var spockDaddy = Instantiate(spockShell, SpawnPosGuide.transform.position, SpawnPosGuide.transform.rotation);

                if (canSpawnSpocks)
                {
                    if (input[1].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(-1, 0, 1));

                        hasSpawned = true;
                    }
                    if (input[2].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(-1, 0, 0));

                        hasSpawned = true;
                    }
                    if (input[3].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(-1, 0, -1));

                        hasSpawned = true;
                    }
                    //temporary hardcode 3x3 grid until foreach is working
                    if (input[4].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(0, 0, 1));
                        hasSpawned = true;
                    }
                    if (input[5].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(0, 0, 0));
                        hasSpawned = true;
                    }
                    if (input[6].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(0, 0, -1));
                        hasSpawned = true;
                    }
                    if (input[7].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(1, 0, 1));
                        hasSpawned = true;
                    }
                    if (input[8].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(1, 0, 0));
                        hasSpawned = true;
                    }
                    if (input[9].ToString() == "1")
                    {
                        Spawn(arrayPos, spockDaddy, new Vector3(1, 0, -1));
                        hasSpawned = true;
                    }
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
            FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); //Sound effect script- this line plays a sound from the AudioManager.
            if (buttonPressed == true && input[0].ToString() == "0") //reset buttonpressed if no input is detected
            {
                buttonPressed = false;
            }
            previousInput = input;
        }
        else
        {
            if (!hasErrored)
            {
                Debug.LogError("Oopsy woopsy arduino got fucky wuckied");
                hasErrored = true;
            }
        }
    }

    void Spawn(int arrayPos, GameObject spockDaddy, Vector3 offset)
    {
        var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
        newSpock.transform.localPosition = offset;

        var spockCollider = spockDaddy.AddComponent<BoxCollider>();
        spockCollider.center = offset;
    }
    void UpdateGhostSpocks(char[] input)
    {
        int ind = 1;
        while (ind < input.Length)
        {
            bool wasActive = ghostSpocks[ind - 1].activeSelf;
            bool isActive = input[ind].ToString() == "1";

            if (isActive && !wasActive)
            {
                ghostSpocks[ind - 1].SetActive(true);
                FindAnyObjectByType<AudioManager>().Play("GhostSpockAppear");
            }
            else if (!isActive && wasActive)
            {
                ghostSpocks[ind - 1].SetActive(false);
                FindAnyObjectByType<AudioManager>().Play("GhostSpockDisappear");
            }

            ind++;
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