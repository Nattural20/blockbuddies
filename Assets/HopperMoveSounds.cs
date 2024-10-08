using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperMoveSounds : MonoBehaviour
{
    public PlayerController hopper;
    private bool moveCueFired = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHopperMovement();
        CheckAnyMovement();
    }

    private void CheckHopperMovement()
    {
        if ((hopper.currentMovementDirection != Vector3.zero) && (moveCueFired == false))
        {
            //hopper moved
            //Debug.Log("Hopper has moved");
            FindAnyObjectByType<AudioManager>().Play("MoveFromStill");
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
}
