using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpawnerCode : MonoBehaviour
{
    public GameObject SpawnPosGuide, spockShell;
    public GameObject[] blocks, ghostSpocks;
    public int arrayPos;
    public bool hasSpawned;
    public bool buttonPressed;
    public bool canSpawnSpocks;
    public TMP_Text blockType;
    public char[] input;
    public char[] previousInput;
    public int spawnLimit = 5;
    public int spockWeight = 60;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();

    bool hasErrored = false;

    /// <summary>
    /// Spawner Code V2. No More Blockin' Around. 
    /// </summary>
    void Update()
    {
        CycleBlocks();
        Spawn();
    }
    public void ChangeSpawnLimit(string input)
    {
        //turn to int
        int x = spawnLimit;//if parse doesnt work, spawnlimit is unchanged
        Int32.TryParse(input, out x);

        spawnLimit = x;
    }

    public void ChangeBlockWeight(string input)
    {
        //turn to int
        int x = spockWeight;//if parse doesnt work, spawnlimit is unchanged
        Int32.TryParse(input, out x);

        spockWeight = x;
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

            if (input[0].ToString() == "1" && !buttonPressed)
            {
                buttonPressed = true;
                GameObject spockDaddy = Instantiate(spockShell, SpawnPosGuide.transform.position, SpawnPosGuide.transform.rotation);

                if (canSpawnSpocks)
                {
                    Vector3[] positions = {
                    new Vector3(-1, 0, 1), new Vector3(-1, 0, 0), new Vector3(-1, 0, -1),
                    new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, -1),
                    new Vector3(1, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, -1)
                };

                    for (int i = 1; i <= 9; i++) //Assign each position active or inactive depending on Arduino input
                    {
                        if (input[i].ToString() == "1")
                        {
                            SpawnBlock(arrayPos, spockDaddy, positions[i - 1]);
                            hasSpawned = true;
                        }
                    }
                }

                //Spawn Limit argument ahead...
                if (spawnQueue.Count >= spawnLimit)
                {
                    GameObject oldestSpockGroup = spawnQueue.Dequeue(); ///Hydrocity Zone Act 1
                    Debug.Log("Destroying oldest spock group: " + oldestSpockGroup.name);
                    Destroy(oldestSpockGroup);
                }
                spawnQueue.Enqueue(spockDaddy); //Queue the newest Spock group after destroying the old one 

                //Debug to see if it actually  works!!
                Debug.Log(hasSpawned ? "Can't spawn just yet." : "No blocks to spawn");//IF '1' :else: '2'

                spockDaddy.GetComponent<Rigidbody>().mass = spockWeight;
                FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); // Sound effect script - this line plays a sound from the AudioManager.
            }

            FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); //Audio Scipt

            if (buttonPressed && input[0].ToString() == "0")
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
                StartCoroutine(ErrorReset());
            }
        }
    }

    void SpawnBlock(int arrayPos, GameObject spockDaddy, Vector3 offset) //spawns each INDIVIDUAL BLOCK within the SpockDaddy group. Gives them a Collider too
    {
        GameObject newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
        newSpock.transform.localPosition = offset;
        BoxCollider spockCollider = spockDaddy.AddComponent<BoxCollider>();
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
            arrayPos = (arrayPos + 1) % blocks.Length; //cycle to 0 after hitting length count
            Debug.Log(arrayPos == 0 ? "Resetting array." : "Changing block.");
        }
    }

    IEnumerator ErrorReset()
    {
        yield return new WaitForSeconds(2);
        hasErrored = false;
    }
}

