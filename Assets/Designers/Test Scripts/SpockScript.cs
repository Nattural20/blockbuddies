using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockScript : MonoBehaviour
{
    //public int[] arduinoInput;
    public int[,] spockLayout;

    public void FormatLayout(int[] griddyInput)
    {
        spockLayout = new int[3, 3];
        
        var griddyPos = 1;
        var posX = 0;
        var posY = 0;

        while (griddyPos < griddyInput.Length)
        {
            if (griddyPos == 4 || griddyPos == 7)
            {
                posX = 0;
                posY++;
            }

            spockLayout[posX,posY] = griddyInput[griddyPos];

            posX++;
            griddyPos++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport Plane"))
        {
            Destroy(gameObject);
        }
    }
}
