using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoLockedSpawn : MonoBehaviour
{
    //public GameObject arduinoController; 
    //private string scriptName = "Spawner.cs";

    private bool _playerPresent = false;
    public SpawnerCodeV3 spawnScript; //use this to get reference to spawner script, should pass in object with the script
    public ArduinoReader readerScript; //use this to get reference to the real
    ///Both of these variables should come from the same object. Fix later.
    //public GameObject spawnLocation;
    public GameObject[] spawnCubes, ghostSpawnCubes; //each cube should be in here

    /// <summary>
    /// This version does not despawn the cubes immediately after leaving the trigger, meaning they are constant. 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject cube in spawnCubes)
        {
            cube.GetComponent<MeshRenderer>().enabled = false;
            cube.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // when are you gonna make Automatic Blast
    void Update()
    {
        if (spawnScript) //null catch
        {

            if (_playerPresent)
            {
                
                SummonBlocks();
            }
            else
            {
                //spawnScript.canSpawnSpocks = true;
            }

            //foreach (var ghost in spawnScript.ghostSpocks)//this sucks
            //{
            //    ghost.SetActive(false);
            //}
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
            spawnScript.canSpawnSpocks = false;

        }
    }

    private void OnTriggerExit(Collider collision) //When the player exits, set to false. 
    {
        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = false;
            //spawnScript.canSpawnSpocks = true; //fixed for v3 spawn code spoof

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
}

