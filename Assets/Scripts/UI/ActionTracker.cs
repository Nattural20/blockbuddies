using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ActionTracker : MonoBehaviour
{
    public PlayerController hopper;
    public AlternateGrab hopperGrab;
    public SpawnerCodeV3 cubert;
    public GameObject instructionUI;

    public int instructionIndex = 0;

    public Animator instructions;

    public int instructionTimeoutTime = 30;

    bool checkingInputs = true;

    void Start()
    {
        hopper = GetComponentInChildren<PlayerController>();
        hopperGrab = GetComponentInChildren<AlternateGrab>();
        cubert = GetComponentInChildren<SpawnerCodeV3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkingInputs)
        {
            if (instructionIndex == 0)
                CheckHopperMove();
            else if (instructionIndex == 1)
                CheckHopperJump();
            //else if (instructionIndex == 2)
            //    CheckSpockUpdate();
            else if (instructionIndex == 2)
                CheckSpockSpawned();
            //else if (instructionIndex == 3)
            //    CheckHopperGrab();

            //if (Input.GetKeyDown(KeyCode.P))
            //{
            //    instructionIndex++;
            //    ChangeInstructions(instructionIndex);
            //}
        }
    }
    
    void CheckHopperMove()
    {
        if (hopper.currentMovementDirection != Vector3.zero)
        {
            //hopper moved
            Debug.Log("Hopper has moved");
            ChangeInstructions();
        }
    }

    void CheckHopperJump()
    {
        if (hopper.isGrounded == false) //temp solution. Can be triggered by false positives, though not likely
        {
            //hopper jump
            Debug.Log("Hopper has jumped");
            ChangeInstructions();
        }
    }

    void CheckSpockUpdate()
    {
        int index = 0;
        foreach (var pos in cubert.input)
        {
            if (index == 0)
                index++;
            else if (pos == cubert.previousInput[index])
            {
                // Spock updated
                Debug.Log("Griddy has changed");
                ChangeInstructions();
                break;
            }
            else
                index++;
        }
    }

    void CheckSpockSpawned()
    {
        if (cubert.hasSpawned == true)
        {
            //spocks spawned
            Debug.Log("Cubert has spawned spock");
            ChangeInstructions();
        }
    }

    void CheckHopperGrab()
    {
        if (hopperGrab.holdingSpock == true)
        {
            //spock held
            Debug.Log("Hopper has grabbed spock");
            ChangeInstructions();
        }
    }

    void ChangeInstructions()
    {
        instructionIndex++;
        // UI Change here
        if (instructionIndex == 3)
        {
            RemoveInstructions();
        }
        else
        {
            StopCoroutine(InstructionTimeout());

            StartCoroutine(InstructionChangeDelay());
            StartCoroutine(InstructionTimeout());

            instructions.SetInteger("nextInstruction", instructionIndex);
        }
    }
    IEnumerator InstructionChangeDelay()
    {
        checkingInputs = false;
        yield return new WaitForSeconds(1);
        checkingInputs = true;
    }

    void RemoveInstructions()
    {
        instructionUI.SetActive(false);
        checkingInputs = false;
        //enabled = false;
    }

    IEnumerator InstructionTimeout()
    {
        int currentInstruction = instructionIndex;
        yield return new WaitForSeconds(instructionTimeoutTime);
        if (instructionIndex == currentInstruction)
        {
            ChangeInstructions();
        }
    }
}
