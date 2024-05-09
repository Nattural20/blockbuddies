using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedSpawn : MonoBehaviour
{
    //public GameObject arduinoController; 
    //private string scriptName = "Spawner.cs";

    private bool _playerPresent = false;
    public SpawnerSpoof spawnScript; //use this to get reference to spawner script, should  pass in object with the script
    public ArduinoReaderSpoof readerScript; //use this to get reference to the spoof output
    ///Both of these variables should come from the same object. Fix later.
    //public GameObject spawnLocation;
    public GameObject[] spawnCubes; //each cube should be in here


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject cube in spawnCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().enabled = false;
        }

    }

    // If i go outside with all these powers
    void Update()
    {
        if (spawnScript) //null catch
        {
            SummonBlocks();
            if (_playerPresent)
            {
                spawnScript.enabled = false;
            }
            else
            {
                spawnScript.enabled = true;
            }
        }
        else
        {
            Debug.Log("Spawn Script not given or cannot be read.");
        }

    }


    private void OnTriggerEnter(UnityEngine.Collider collision) //When the player enters, set player to true. 
    {
        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider collision) //When the player exits, set to false. 
    {
        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = false;
        }
    }
    private void SummonBlocks() //When the flibby flubbers, set to Go-Blon. 
    {
        //throw new NotImplementedException();
        int[] outputArray = readerScript.OutputArray;


        int index = 0;

        if (_playerPresent)
        {
            foreach (int i in outputArray)
            {
                index++;

                if (i == 1)
                {
                    Debug.Log("Array Length:" + outputArray.Length);
                    //Debug.Log("Postion: " + index + ". Spawning Block: " + spawnCubes[index]);
                    spawnCubes[index].GetComponent<MeshRenderer>().enabled = true;
                    spawnCubes[index].GetComponent<BoxCollider>().enabled = true;
                }
                else
                {
                    spawnCubes[index].GetComponent<MeshRenderer>().enabled = false;
                    spawnCubes[index].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
        else
        {
            foreach (int i in outputArray)
            {
                index++;
                {
                    spawnCubes[index].GetComponent<MeshRenderer>().enabled = false;
                    spawnCubes[index].GetComponent<BoxCollider>().enabled = false;
                }
            }

        }
    }
}   


