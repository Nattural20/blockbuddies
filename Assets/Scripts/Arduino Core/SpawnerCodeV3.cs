using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class SpawnerCodeV3 : MonoBehaviour
{
    public bool enableDebugSpam = true;
    public GameObject SpawnPosGuide, spockShell;
    public GameObject[] blocks, ghostSpocks;
    public int arrayPos;
    public bool hasSpawned;
    public bool buttonPressed;
    public bool canSpawnSpocks;
    public TMP_Text blockType;
    public char[] input;
    public char[] previousInput;
    public int spawnLimit = 5;
    public int spockWeight = 60;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();
    bool hasErrored = false;

    public Toggle spoofToggle;
    private bool TogOn = false;

    [SerializeField] public Dropdown spawnRotation;
    public readonly string[] options = { "Left", "Up", "Right", "Down" };

    //Spoof arguments
    public bool enableSpoof = false;
    public Image[] spockDisplay;
    public char[] SpoofOutputArray;
    public GameObject SpoofCanvas;

    //Rumble properties
    public bool rumble = true;
    public float lowFrequency = .100f;
    public float highFrequency = .750f;
    public float rumbleDuration = .1f;
    private bool haptics = false;
    private DualShockGamepad dualShockGamepad;
    private Gamepad currentGamepad;
    bool isDualshock;

    //End conditions
    private EndViewTrigger endViewTrigger;
    private bool unlimited;

    /// <summary>
    /// Spawner Code V3.Integrated Spoof toggle and functionality.  
    /// </summary>
    public void Start()
    {
        endViewTrigger = GameObject.Find("EndViewTrigger").GetComponent<EndViewTrigger>();
    }

    void Update()
    {
        CycleBlocks();
        Spawn();

        if (enableSpoof)
        {
            SpoofCanvas.SetActive(true); ///Paul Blart Mall Cop
        }
        else
        {
            SpoofCanvas.SetActive(false);
        }

        ToggleSpoof();

        
        if (endViewTrigger != null)
        {
            unlimited = endViewTrigger.unlimited;

            if (unlimited)
            {
                spawnLimit = 9999999;
            }
        }


    }

    void Spawn()
    {


        input = GetInput();


        //I am so sorry about the sins I have committed 
        //No I'm not. Those sins were pointless!!!
        //Left Orientation
        if (input != null)
        {
            if (enableDebugSpam)
                Debug.Log("Facing Left");
            if (canSpawnSpocks)
            {
                UpdateGhostSpocks(input);
            }

            if (input[0].ToString() == "1" && !buttonPressed)
            {
                if (Gamepad.current is DualShockGamepad)
                {
                    isDualshock = true;
                    dualShockGamepad = (DualShockGamepad)Gamepad.current;
                    dualShockGamepad.SetLightBarColor(Color.magenta);
                }
                
                buttonPressed = true;
                GameObject spockDaddy = Instantiate(spockShell, SpawnPosGuide.transform.position, SpawnPosGuide.transform.rotation);
                if (rumble == true)
                {
                    TriggerHapticFeedback();
                }

                if (canSpawnSpocks)
                {
                    Vector3[] positions = {
                    new Vector3(-1, 0, 1), new Vector3(-1, 0, 0), new Vector3(-1, 0, -1),
                    new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(0, 0, -1),
                    new Vector3(1, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, -1)
                };

                    for (int i = 1; i <= 9; i++) //Assign each position active or inactive depending on Arduino input
                    {
                        if (input[i].ToString() == "1")
                        {
                            SpawnBlock(arrayPos, spockDaddy, positions[i - 1]);
                            hasSpawned = true;


                        }
                    }
                }

                //Spawn Limit argument ahead...
                if (spawnQueue.Count >= spawnLimit)
                {
                    GameObject oldestSpockGroup = spawnQueue.Dequeue(); ///Mall Blart Pall Cop
                    Debug.Log("Destroying oldest spock group: " + oldestSpockGroup.name);
                    Destroy(oldestSpockGroup);
                }
                spawnQueue.Enqueue(spockDaddy); //Queue the newest Spock group after destroying the old one 

                //Debug to see if it actually  works!!
                Debug.Log(hasSpawned ? "Can't spawn just yet." : "No blocks to spawn");//IF '1' :else: '2'

                spockDaddy.GetComponent<Rigidbody>().mass = spockWeight;
                if (canSpawnSpocks)
                {
                    FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); // Sound effect script - this line plays a sound from the AudioManager.
                }
            }

            //FindAnyObjectByType<AudioManager>().Play("SpockSpawn"); //Audio Scipt

            if (buttonPressed && input[0].ToString() == "0")
            {

                buttonPressed = false;
            }
            else if (!buttonPressed && input[0].ToString() == "0" && isDualshock)
            {
                dualShockGamepad.SetLightBarColor(Color.blue);
            }

            previousInput = input;
        }

        
        else
        {
            if (!hasErrored)
            {
                Debug.LogError("Oopsy woopsy arduino got fucky wuckied");
                hasErrored = true;
                StartCoroutine(ErrorReset());
            }
        }
    }

    void SpawnBlock(int arrayPos, GameObject spockDaddy, Vector3 offset) //spawns each INDIVIDUAL BLOCK within the SpockDaddy group. Gives them a Collider too
    {
        GameObject newSpock = Instantiate(blocks[arrayPos], spockDaddy.transform);
        newSpock.transform.localPosition = offset;
        BoxCollider spockCollider = spockDaddy.AddComponent<BoxCollider>();
        spockCollider.center = offset;
        

    }

    void UpdateGhostSpocks(char[] input)
    {
        int ind = 1;
        while (ind < input.Length)
        {
            bool wasActive = ghostSpocks[ind - 1].activeSelf;
            bool isActive = input[ind].ToString() == "1";

            if (isActive && !wasActive)
            {
                ghostSpocks[ind - 1].SetActive(true);
                FindAnyObjectByType<AudioManager>().Play("GhostSpockAppear");
            }
            else if (!isActive && wasActive)
            {
                ghostSpocks[ind - 1].SetActive(false);
                FindAnyObjectByType<AudioManager>().Play("GhostSpockDisappear");
            }

            ind++;
        }
    }

    void CycleBlocks()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            arrayPos = (arrayPos + 1) % blocks.Length; //cycle to 0 after hitting length count
            Debug.Log(arrayPos == 0 ? "Resetting array." : "Changing block.");
        }
    }

    char[] GetInput() //new inputs: works normally if normal, redirect to spoof if spoof is enabled
    {
        if (!enableSpoof)
        {
            input = GetComponent<ArduinoReader>().OutputArray;
        }
        else
        {
            input = SpoofReadIn();
        }
        return input;
    }

    private char[] SpoofReadIn() //the whole spoof reader function. Not the best solution, but it works
    {
        //'button input'
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpoofOutputArray[0] = '1';
        }
        else
        {
            SpoofOutputArray[0] = '0';
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1)) { if (SpoofOutputArray[1] == '1') { SpoofOutputArray[1] = '0'; spockDisplay[0].color = Color.red;  } else { SpoofOutputArray[1] = '1'; spockDisplay[0].color = Color.green;  } }//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2)) { if (SpoofOutputArray[2] == '1') { SpoofOutputArray[2] = '0'; spockDisplay[1].color = Color.red;  } else { SpoofOutputArray[2] = '1'; spockDisplay[1].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3)) { if (SpoofOutputArray[3] == '1') { SpoofOutputArray[3] = '0'; spockDisplay[2].color = Color.red;  } else { SpoofOutputArray[3] = '1'; spockDisplay[2].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4)) { if (SpoofOutputArray[4] == '1') { SpoofOutputArray[4] = '0'; spockDisplay[3].color = Color.red; } else { SpoofOutputArray[4] = '1'; spockDisplay[3].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5)) { if (SpoofOutputArray[5] == '1') { SpoofOutputArray[5] = '0'; spockDisplay[4].color = Color.red; } else { SpoofOutputArray[5] = '1'; spockDisplay[4].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6)) { if (SpoofOutputArray[6] == '1') { SpoofOutputArray[6] = '0'; spockDisplay[5].color = Color.red;  } else { SpoofOutputArray[6] = '1'; spockDisplay[5].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7)) { if (SpoofOutputArray[7] == '1') { SpoofOutputArray[7] = '0'; spockDisplay[6].color = Color.red;  } else { SpoofOutputArray[7] = '1'; spockDisplay[6].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8)) { if (SpoofOutputArray[8] == '1') { SpoofOutputArray[8] = '0'; spockDisplay[7].color = Color.red;  } else { SpoofOutputArray[8] = '1'; spockDisplay[7].color = Color.green;  }}//griddyPlace.Play(); } }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9)) { if (SpoofOutputArray[9] == '1') { SpoofOutputArray[9] = '0'; spockDisplay[8].color = Color.red;  } else { SpoofOutputArray[9] = '1'; spockDisplay[8].color = Color.green;  }}//griddyPlace.Play(); } }

        if (enableDebugSpam)
            Debug.Log("Sending " + SpoofOutputArray.Length + "chars");

        return SpoofOutputArray;
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
        int x = spockWeight;//if parse doesnt work, blockweight is unchanged
        Int32.TryParse(input, out x);

        spockWeight = x;
    }
    IEnumerator ErrorReset()
    {
        yield return new WaitForSeconds(2);
        hasErrored = false;
    }

    public void ToggleSpoof()
    {
        // Check if the Toggle is on
        if (spoofToggle.isOn && !TogOn)
        {
            enableSpoof = true;
            TogOn = true;
            Debug.Log("Spoofing enabled.");
        }
        // If the Toggle is off and it was previously on, disable the spoofing
        else if (!spoofToggle.isOn && TogOn)
        {
            enableSpoof = false;
            TogOn = false;
            Debug.Log("Spoofing disabled.");
        }
    }

    public void ToggleSpoofBool(bool input)
    {
        // Check if the Toggle is on
        if (input == true)
        {
            enableSpoof = true;
            TogOn = true;
            Debug.Log("Spoofing enabled.");
        }
        // If the Toggle is off and it was previously on, disable the spoofing
        else if (input == false)
        {
            enableSpoof = false;
            TogOn = false;
            Debug.Log("Spoofing disabled.");
        }
    }

    public void TriggerHapticFeedback()
    {
        // catch to stop error messages in console
        if (haptics == true)
        {
            Debug.Log("Interupting Rumble");
            currentGamepad.SetMotorSpeeds(0f, 0f);
            haptics = false;
        }

        if (currentGamepad != null && haptics == false)
        {
            haptics = true;
            StartCoroutine(HapticCoroutine());
        }
    }

    private IEnumerator HapticCoroutine()
    {
        Debug.Log("I'm about to rumble");
        // Set the motor speeds to start the haptic feedback.
        currentGamepad.SetMotorSpeeds(lowFrequency, highFrequency);
       
        // Wait for set duration.
        yield return new WaitForSeconds(rumbleDuration);

        Debug.Log("Rumble stopping");
        // Reset haptic feedback.
        currentGamepad.ResetHaptics();
        haptics = false;
    }
}


