using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

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
    public bool onScreen, screenTop;
    LilyLookSelection lookSelection;
    bool readyToSpawn = true;

    /// <summary>
    /// This version does not despawn the cubes immediately after leaving the trigger, meaning they are constant. 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        lookSelection = GetComponentInChildren<LilyLookSelection>();
        foreach (GameObject cube in spawnCubes)
        {
            cube.SetActive(false);
        }
        foreach (GameObject ghost in ghostSpawnCubes)
        {
            ghost.SetActive(false);
        }
    }

    // when are you gonna make Automatic Blast
    void FixedUpdate()
    {
        if (spawnScript) //null catch
        {
            if (!onScreen)
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
        if (spawnScript.enableSpoof == true)//check for spoof output
        {
            outputArray = spawnScript.SpoofOutputArray;
        }

        UpdateLockedGhostCubes(outputArray);

        if (outputArray[0].ToString() == "1" && spawnScript.buttonPressed == true && readyToSpawn == true)
        {
            readyToSpawn = false;

            FindAnyObjectByType<AudioManager>().Play("SpockSpawn");

            int index = 0;


            //foreach (GameObject cube in spawnCubes)
            //{
            //    cube.SetActive(false);
            //}

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
                        if (spawnCubes[index-1].activeSelf == false)
                        {
                            spawnCubes[index - 1].SetActive(true);
                        }
                    }
                    index++;
                }
                else
                {
                    spawnCubes[index - 1].SetActive(false);
                    index++;
                }
            }
        }
        else if (outputArray[0].ToString() == "0")
        {
            readyToSpawn = true;
        }
    }
    private void UpdateLockedGhostCubes(char[] outputArray)
    {
        int index = 0;

        int ind = 1;
        while (ind < outputArray.Length)
        {
            bool wasActive = ghostSpawnCubes[ind - 1].activeSelf;
            bool isActive = outputArray[ind].ToString() == "1";

            if (isActive && !wasActive)
            {
                FindAnyObjectByType<AudioManager>().Play("GhostSpockAppear");
            }
            else if (!isActive && wasActive)
            {
                FindAnyObjectByType<AudioManager>().Play("GhostSpockDisappear");
            }

            ind++;
        }




        foreach (char i in outputArray)
        {
            if (index == 0)
            {
                index++;
            }
            else if (i.ToString() == "1")
            {
                ghostSpawnCubes[index - 1].SetActive(true);

                spawnCubes[index - 1].GetComponent<SpockColourChange>().ChangeToBlue();

                index++;
            }
            else
            {
                ghostSpawnCubes[index - 1].SetActive(false);

                spawnCubes[index - 1].GetComponent<SpockColourChange>().ChangeToRed();

                index++;
            }
        }
    }
    public void BustGhosts()
    {
        foreach (GameObject ghost in ghostSpawnCubes)
        {
            ghost.SetActive(false);
        }
    }
    public void BustSpocks()
    {
        foreach (GameObject cube in spawnCubes)
        {
            cube.SetActive(false);
        }
    }
}

