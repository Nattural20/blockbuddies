using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.Animations;
using UnityEngine;

public class ActionTracker : MonoBehaviour
{
    public PlayerController hopper;
    public AlternateGrab hopperGrab;
    public SpawnerSpoofUnspoof cubert;

    public int instructionIndex = 0;

    public Animator instructions;

    bool checkingInputs = true;

    void Start()
    {
        hopper = GetComponentInChildren<PlayerController>();
        hopperGrab = GetComponentInChildren<AlternateGrab>();
        cubert = GetComponentInChildren<SpawnerSpoofUnspoof>();
        instructions = GetComponentInChildren<Animator>();
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
            else if (instructionIndex == 2)
                CheckSpockUpdate();
            else if (instructionIndex == 3)
                CheckSpockSpawned();
            else if (instructionIndex == 4)
                CheckHopperGrab();
        }
    }
    
    void CheckHopperMove()
    {
        if (hopper.currentMovementDirection != Vector3.zero)
        {
            //hopper moved
            Debug.Log("Hopper has moved");
            ChangeInstructions(1);

            instructionIndex++;
        }
    }

    void CheckHopperJump()
    {
        if (hopper.isGrounded == false) //temp solution. Can be triggered by false positives, though not likely
        {
            //hopper jump
            Debug.Log("Hopper has jumped");
            ChangeInstructions(2);

            instructionIndex++;
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
                ChangeInstructions(3);

                instructionIndex++;
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
            ChangeInstructions(4);

            instructionIndex++;
        }
    }

    void CheckHopperGrab()
    {
        if (hopperGrab.holdingSpock == true)
        {
            //spock held
            Debug.Log("Hopepr has grabbed spock");
            ChangeInstructions(5);

            instructionIndex++;
        }
    }

    void ChangeInstructions(int next)
    {
        // UI Change here
        instructions.SetInteger("nextInstruction", next);
    }
}
