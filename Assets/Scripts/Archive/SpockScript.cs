using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockScript : MonoBehaviour
{
    //public int[] arduinoInput;
    public int[,] spockLayout;
    public float spockAmount;

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

            spockLayout[posY,posX] = griddyInput[griddyPos];

            if (griddyInput[griddyPos] == 1)
                spockAmount++;

            posX++;
            griddyPos++;
        }
        Debug.Log(spockLayout[2,0] + ", " + spockLayout[2, 1] + ", " + spockLayout[2, 2] + "\n" + spockLayout[1,0] + ", " + spockLayout[1, 1] + ", " + spockLayout[1, 2] + "\n" + spockLayout[0, 0] + ", " + spockLayout[0, 1] + ", " + spockLayout[0, 2]);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport Plane"))
        {
            Destroy(gameObject);
        }
    }
}
