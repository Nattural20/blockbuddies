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
    public Material[] spockGlow;

    char[] currentSpawns;

    int triggerCount;



    /// <summary>
    /// This version does not despawn the cubes immediately after leaving the trigger, meaning they are constant. 
    /// </summary>

    private CubertMovement cM;

    private void Awake()
    {
        cM = GameObject.Find("CUBERT").GetComponent<CubertMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpawns = new char[10];
        foreach (GameObject cube in spawnCubes)
        {
            cube.SetActive(false);
            //cube.GetComponent<MeshRenderer>().enabled = false;
            //cube.GetComponent<BoxCollider>().enabled = false;
        }
        foreach (GameObject ghost in ghostSpawnCubes)
        {
            ghost.SetActive(false);
            //ghost.GetComponent<MeshRenderer>().enabled = false;
            //ghost.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // when are you gonna make Automatic Blast
    void FixedUpdate()
    {
        if (_playerPresent)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                spawnCubes[0].GetComponent<SpockColourChange>().ChangeToBlue();
                Debug.Log("Changing spock material to blue");
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                spawnCubes[0].GetComponent<SpockColourChange>().ChangeToRed();
                Debug.Log("Changing spock material to red");
            }
        }

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

        }
        else
        {
            Debug.Log("Spawn Script not given or cannot be read.");
        }

    }


    private void OnTriggerEnter(UnityEngine.Collider collision) //When the player enters, set player to true. 
    {
        cM.currentSpawnLockPosition = this.gameObject.transform;
        cM.cubertOnLock = true;


        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = true;
            spawnScript.canSpawnSpocks = false;

            triggerCount++;

            Debug.Log("Player is present is " + triggerCount + " triggers");
        }
    }

    private void OnTriggerExit(Collider collision) //When the player exits, set to false. 
    {
        cM.cubertOnLock = false;


        if (collision.gameObject.tag == "Body")
        {
            triggerCount--;
            if (triggerCount == 0)
            {
                _playerPresent = false;
            }
            //spawnScript.canSpawnSpocks = true; //fixed for v3 spawn code spoof
            Debug.Log("Player has left. Current trigger count is " + triggerCount + " triggers");

        }
    }
    private void SummonBlocks() //summons the blocks
    {
        //throw new NotImplementedException();
        char[] outputArray = readerScript.OutputArray;
        if (spawnScript.enableSpoof == true)//check for spoof output
        {
            outputArray = spawnScript.SpoofOutputArray;
        }

            UpdateLockedGhostCubes(outputArray);

        if (outputArray[0].ToString() == "1" && spawnScript.buttonPressed == true)
        {
            //currentSpawns = outputArray;

            int index = 0;


            foreach (GameObject cube in spawnCubes)
            {
                cube.SetActive(false);
                //cube.GetComponent<MeshRenderer>().enabled = false;
                //cube.GetComponent<BoxCollider>().enabled = false;
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
                        spawnCubes[index - 1].SetActive(true);
                        //spawnCubes[index - 1].GetComponent<MeshRenderer>().enabled = true;
                        //spawnCubes[index - 1].GetComponent<BoxCollider>().enabled = true;
                    }
                    index++;
                }
                else
                {
                    index++;
                }
                FindAnyObjectByType<AudioManager>().Play("SpockSpawn");
            }
        }
    }
    private void UpdateLockedGhostCubes(char[] outputArray)
    {
        int index = 0;


        //foreach (GameObject cube in ghostSpawnCubes)
        //{
        //    cube.GetComponent<MeshRenderer>().enabled = false;
        //}
        foreach (char i in outputArray)
        {
            if (index == 0)
            {
                index++;
            }
            else if (i.ToString() == "1")
            {
                ghostSpawnCubes[index - 1].SetActive(true); //GetComponent<MeshRenderer>().enabled = true;

                spawnCubes[index - 1].GetComponent<SpockColourChange>().ChangeToBlue();

                index++;
            }
            else
            {
                ghostSpawnCubes[index - 1].SetActive(false); //GetComponent<MeshRenderer>().enabled = false;

                spawnCubes[index - 1].GetComponent<SpockColourChange>().ChangeToRed();

                index++;
            }
        }
    }
}

