using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLockedSpawn : MonoBehaviour
{
    //public GameObject arduinoController; 
    //private string scriptName = "Spawner.cs";

    private bool _playerPresent = false;
    public SpawnerSpoof spawnScript; //use this to get reference to spawner script, should  pass in object with the script
    public ArduinoReaderSpoof readerScript; //use this to get reference to the spoof output
    ///Both of these variables should come from the same object. Fix later.
    //public GameObject spawnLocation;
    public GameObject[] spawnCubes; //each cube should be in here

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

    // Purgatori
    void Update()
    {
        if (spawnScript) //null catch
        {

            if (_playerPresent)
            {
                spawnScript.enabled = false;
                SummonBlocks();
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
    private void SummonBlocks() //Congregation jumpscare 
    {
        //throw new NotImplementedException();
        int[] outputArray = readerScript.OutputArray;


        int index = 0;

        if (Input.GetKeyDown(KeyCode.F)) //make this whatever value the button is- int[0], char, whatever
        {
            foreach (GameObject cube in spawnCubes)
            {
                cube.GetComponent<MeshRenderer>().enabled = false;
                cube.GetComponent<BoxCollider>().enabled = false;
            }
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
            }
        }
    }
}
