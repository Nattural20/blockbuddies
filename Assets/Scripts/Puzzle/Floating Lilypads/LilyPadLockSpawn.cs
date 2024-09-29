using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadLockedSpawn : MonoBehaviour
{
    //public GameObject arduinoController; 
    //private string scriptName = "Spawner.cs";

    public bool _playerPresent = false;
    public SpawnerCodeV3 spawnScript; //use this to get reference to spawner script, should pass in object with the script
    public ArduinoReader readerScript; //use this to get reference to the real
    ///Both of these variables should come from the same object. Fix later.
    //public GameObject spawnLocation;
    public GameObject[] spawnCubes, ghostSpawnCubes; //each cube should be in here
    public GameObject spawnCubesParent;

    public float selectionDistance = 20;
    public float padDistance;
    public float centerDistance;
    public bool onScreen;
    LilyLookSelection lookSelection;

    /// <summary>
    /// This version does not despawn the cubes immediately after leaving the trigger, meaning they are constant. 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        lookSelection = GetComponentInChildren<LilyLookSelection>();
        foreach (GameObject cube in spawnCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().enabled = false;
        }
        foreach (GameObject ghost in ghostSpawnCubes)
        {
            ghost.GetComponent<MeshRenderer>().enabled = false;
            ghost.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // when are you gonna make Automatic Blast
    void Update()
    {
        if (spawnScript) //null catch
        {
            if (onScreen)
            {
                //if (padDistance < selectionDistance)
                //{
                //    _playerPresent = true;
                //    spawnScript.canSpawnSpocks = false;
                //}
            }
            else
            {
                _playerPresent = false;
            }
            if (_playerPresent)
            {
                
                SummonBlocks();
            }
            else
            {
                //spawnScript.canSpawnSpocks = true;
            }
        }
        else
        {
            Debug.Log("Spawn Script not given or cannot be read.");
        }

    }
    private void SummonBlocks() //summons the blocks
    {
        //throw new NotImplementedException();
        char[] outputArray = readerScript.OutputArray;
        if (outputArray == null)//check for spoof output
        {
            outputArray = spawnScript.SpoofOutputArray;
        }

        UpdateLockedGhostCubes(outputArray);

        if (outputArray[0].ToString() == "1" && spawnScript.buttonPressed == true)
        {

            int index = 0;


            foreach (GameObject cube in spawnCubes)
            {
                cube.GetComponent<MeshRenderer>().enabled = false;
                cube.GetComponent<BoxCollider>().enabled = false;
            }
            foreach (char i in outputArray)
            {
                if (index == 0)
                {
                    index++;
                }
                else if (i.ToString() == "1")
                {
                    //Debug.Log("Array Length:" + outputArray.Length);
                    //Debug.Log("Postion: " + index + ". Spawning Block: " + spawnCubes[index]);

                    if (ghostSpawnCubes[index -1].GetComponent<SpockSpawnPlayerDetector>().playerPresent == false)
                    {
                        //Move the spawnCubes stuff into here to have it not spawn if a player is in the way
                        spawnCubes[index - 1].GetComponent<MeshRenderer>().enabled = true;
                        spawnCubes[index - 1].GetComponent<BoxCollider>().enabled = true;
                    }
                    index++;
                }
                else
                {
                    index++;
                }
            }
        }
    }
    private void UpdateLockedGhostCubes(char[] outputArray)
    {
        int index = 0;


        foreach (GameObject cube in ghostSpawnCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
        }
        foreach (char i in outputArray)
        {
            if (index == 0)
            {
                index++;
            }
            else if (i.ToString() == "1")
            {
                ghostSpawnCubes[index - 1].GetComponent<MeshRenderer>().enabled = true;
                index++;
            }
            else
            {
                index++;
            }
        }
    }
    public void BustGhosts()
    {
        foreach (GameObject ghost in ghostSpawnCubes)
        {
            ghost.GetComponent<MeshRenderer>().enabled = false;
            ghost.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void BustSpocks()
    {
        foreach (GameObject cube in spawnCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

