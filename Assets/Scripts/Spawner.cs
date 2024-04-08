using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    //Gets player object
    public GameObject fauxPlayer;
    public GameObject gameController;


    public UnityEngine.UI.Button normalButton;
    public UnityEngine.UI.Button icyButton;
    public UnityEngine.UI.Button bouncyButton;

    //Stores the blocks in their own seperate arrays
    public GameObject[] normalBlocks;
    public GameObject[] iceBlocks;
    public GameObject[] bounceBlocks;

    
    public int arrayPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(UnityEngine.UI.Button normalButton, UnityEngine.UI.Button icyButton, UnityEngine.UI.Button bouncyButton)
    {
        //Buttons to change the tag of the Game Controller.
    }


    // Update is called once per frame
    void Update()
    {
        //Read the set tag to dertermine the array to be spawned from.

        //In all permeations, right click sets player into spawn mode
        //Tab cycles through the pieces in the array
        //Left click to spawn object.
        if (gameController.CompareTag("Normal"))
        {
            
            Debug.Log("Set to Normal");
            //Sets the player to spawn mode
            if (Input.GetMouseButton(1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(normalBlocks[arrayPos]);
                }

                //Cycles through the blocks in the given array
                if (Input.GetKeyDown(KeyCode.Tab))
                {
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
    }
}

