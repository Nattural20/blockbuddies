using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperMoveSounds : MonoBehaviour
{
    public PlayerController hopper;
    private bool moveCueFired = false;
    private bool inWater = false; 

    void Start()
    {
    }

    void Update()
    {
        CheckHopperMovement();
        CheckAnyMovement();
        if (inWater)
        {
            Debug.Log("WATERWATERWATERWATER");
        }
    }

    private void CheckHopperMovement()
    {
        if ((hopper.currentMovementDirection != Vector3.zero) && (moveCueFired == false))
        {
            //Play different sound if in water
            if (inWater)
            {
                FindAnyObjectByType<AudioManager>().Play("PlayerInWater");
            }
            else
            {
                FindAnyObjectByType<AudioManager>().Play("MoveFromStill");
            }

            moveCueFired = true;
        }
    }

    private void CheckAnyMovement()
    {
        if (hopper.currentMovementDirection == Vector3.zero)
        {
            moveCueFired = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Body"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Body"))
        {
            inWater = false; //REMOVE TAG when leaving water trigger
        }
    }
}

