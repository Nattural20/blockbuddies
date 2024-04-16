using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;

public class Spawner : MonoBehaviour
{
    //Gets player object
    public GameObject player;
    public GameObject gameController;
    public Camera Camera;
/*    public GameObject displayCamera;*/


/*    public UnityEngine.UI.Button normalButton;
    public UnityEngine.UI.Button icyButton;
    public UnityEngine.UI.Button bouncyButton;

    //Stores the blocks in their own seperate arrays
    public GameObject[] normalBlocks;
    public GameObject[] iceBlocks;
    public GameObject[] bounceBlocks;*/

    public GameObject[] blocks;

    
    public int arrayPos;

    public bool pos1;
    public bool pos2;
    public bool pos3;

    public bool hasJoint = false;

    public TMP_Text blockType;

/*    private bool isDisplayActive = false;*/



    //Arduino will dictate which blocks spawn, implement TAB to change block properties.
    //If blocks are spawned nect to each other, add a joint.



    // Start is called before the first frame update
    void Start()
    {
        
    }

/*    public void OnClick(UnityEngine.UI.Button normalButton, UnityEngine.UI.Button icyButton, UnityEngine.UI.Button bouncyButton)
    {
        //Buttons to change the tag of the Game Controller.
    }*/


    // Update is called once per frame
    void Update()
    {
        Spawn();

        CycleBlocks();

        DisplayPos();
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
        else
        {
            Debug.Log("Error");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>().CompareTag("Spocks") && !hasJoint) 
        {
            gameObject.AddComponent<FixedJoint>();
            gameObject.GetComponent<FixedJoint>().connectedBody=collision.rigidbody;
            hasJoint = true;
        }
    }


    void Spawn()
    {

            if (Input.GetMouseButtonDown(0))
            {
                if (pos1 == true)
                {
                 Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, -1), Camera.transform.rotation);
                    
                    
                }
                if (pos2 == true)
                {
                    Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, 0), Camera.transform.rotation);
                }
                if (pos3 == true)
                {
                    Instantiate(blocks[arrayPos], Camera.transform.position + new Vector3(4, 1, 1), Camera.transform.rotation);
                }


                else
                {
                    Debug.Log("No blocks to spawn");
                }
            }


    }


    void CycleBlocks()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //*ChangeDisplay();*//*
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
        //Read the set tag to dertermine the array to be spawned from.

        //In all permeations, right click sets player into spawn mode
        //Tab cycles through the pieces in the array
       /* //Left click to spawn object.
        if (gameController.CompareTag("Normal"))
        {
            
            Debug.Log("Set to Normal");
            //Sets the player to spawn mode
            if (Input.GetMouseButton(1))
            {
*//*                if (isDisplayActive == false)
                {
                    SpawnDisplay();
                    *//*isDisplayActive = true;*//*
                }
                else if (isDisplayActive == true)
                {
                    
                }*//*

                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(normalBlocks[arrayPos]);
                }

                //Cycles through the blocks in the given array
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    *//*ChangeDisplay();*//*
                    arrayPos += 1;
                    Debug.Log("Changing ice block.");
                    if (arrayPos >= normalBlocks.Length)
                    {
                        //Array resets and loops
                        arrayPos = 0;
                        Debug.Log("Resetting array.");
                    }
                }
            }
        }

        if (gameController.CompareTag("Icy"))
        {

            Debug.Log("Set to Icy");
            //Sets the player to spawn mode
            if (Input.GetMouseButton(1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(iceBlocks[arrayPos]);
                }

                //Cycles through the blocks in the given array
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    arrayPos += 1;
                    Debug.Log("Changing ice block.");
                    if (arrayPos >= iceBlocks.Length)
                    {
                        //Array resets and loops
                        arrayPos = 0;
                        Debug.Log("Resetting array.");
                    }
                }
            }
        }

        if (gameController.CompareTag("Bouncy"))
        {

            Debug.Log("Set to Bouncy");

            //Sets the player to spawn mode
            if (Input.GetMouseButton(1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(bounceBlocks[arrayPos]);
                }

                //Cycles through the blocks in the given array
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    arrayPos += 1;
                    Debug.Log("Changing ice block.");
                    if (arrayPos >= bounceBlocks.Length)
                    {
                        //Array resets and loops
                        arrayPos = 0;
                        Debug.Log("Resetting array.");
                    }
                }


            }
        }
    }*/

/*    void SpawnDisplay()
    {
        GameObject display = GameObject.Instantiate(normalBlocks[arrayPos], new Vector3(0, 2000, 100), Quaternion.identity);
        display.GetComponent<Rigidbody>().isKinematic = true;


        Instantiate(displayCamera, new Vector3(0, 2000, 80), Quaternion.identity);

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Destroy(display);
            arrayPos += 1;
            if (arrayPos >= bounceBlocks.Length)
            {
                //Array resets and loops
                arrayPos = 0;
            }
                SpawnDisplay();
        }
    }*/


/*    void ChangeDisplay()
    {
        GameObject display = SpawnDisplay(display);
    }*/

}

