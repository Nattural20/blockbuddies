using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SpawnerSpoof : MonoBehaviour
{
    public GameObject SpawnPosGuide, spockShell;
    public GameObject[] blocks;
    public int arrayPos;
    private bool hasSpawned;
    public TMP_Text blockType;
    public int spawnLimit = 5;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();
    public int spockWeight = 60;

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
        int[] input = GetComponent<ArduinoReaderSpoof>().OutputArray;
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject spockDaddy = Instantiate(spockShell, SpawnPosGuide.transform.position, SpawnPosGuide.transform.rotation);
            

            var daddyList = spockDaddy.GetComponent<SpockScript>();
            daddyList.FormatLayout(input);

            SpawnSpock(input, spockDaddy);

            if (spawnQueue.Count >= spawnLimit)
            {
                GameObject oldestSpockGroup = spawnQueue.Dequeue();
                Debug.Log("Destroying oldest spock group: " + oldestSpockGroup.name);
                Destroy(oldestSpockGroup);
            }

            // Add spockDaddy to the queue
            spawnQueue.Enqueue(spockDaddy);
            spockDaddy.GetComponent<Rigidbody>().mass = spockWeight;
            FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); // Sound effect script - this line plays a sound from the AudioManager.
        }
    }

    void SpawnSpock(int[] input, GameObject spockDaddy)
    {
        Vector3[] positions = {
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, 0),
            new Vector3(-1, 0, -1),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, -1)
        };

        for (int i = 1; i <= 9; i++)
        {
            if (input[i].ToString() == "1")
            {
                Spawn(arrayPos, spockDaddy, positions[i - 1]);
                hasSpawned = true;
            }
        }

        if (hasSpawned)
        {
            Debug.Log("Can't spawn just yet.");
        }
        else
        {
            Debug.Log("No blocks to spawn");
            hasSpawned = false;
        }
    }

    void Spawn(int arrayPos, GameObject spockDaddy, Vector3 offset)
    {
        var newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
        newSpock.transform.localPosition = offset;
        var spockCollider = spockDaddy.AddComponent<BoxCollider>();
        spockCollider.center = offset;
        Debug.Log("Spawned new spock: " + newSpock.name);
    }

    void CycleBlocks()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            arrayPos += 1;
            Debug.Log("Changing block.");
            if (arrayPos >= blocks.Length)
            {
                // Array resets and loops
                arrayPos = 0;
                Debug.Log("Resetting array.");
            }
        }
    }
}
