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
        cM.currentSpawnLockPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z) + this.gameObject.transform.forward * 2; ;
        cM.cubertOnLock = true;


        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = true;
            spawnScript.canSpawnSpocks = false;

            triggerCount++;
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
            }
            foreach (char i in outputArray)
            {
                if (index == 0)
                {
                    index++;
                }
                else if (i.ToString() == "1")
                {
                    if (ghostSpawnCubes[index -1].GetComponent<SpockSpawnPlayerDetector>().playerPresent == false)
                    {
                        spawnCubes[index - 1].SetActive(true);
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
    public void RemoveSpocks()
    {
        foreach (GameObject cube in spawnCubes)
        {
            cube.SetActive(false);
        }
    }
}

