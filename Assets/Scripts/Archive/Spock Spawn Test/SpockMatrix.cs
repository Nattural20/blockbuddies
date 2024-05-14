using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class SpockMatrix : MonoBehaviour
{
    public int[,] spockLayout;
    public float spockAmount;

    public static SpockConnections[,] CheckConnections(int[] griddyInput)
    {
        var arrayRef = new List<SpockArrayRefer>();
        var spockLayout = new int[3, 3];
        
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

            spockLayout[posY, posX] = griddyInput[griddyPos];
            SpockArrayRefer referenceArray = new SpockArrayRefer();

            referenceArray.ArrayPos2D = new Vector2(posX, posY);
            referenceArray.griddayArrayPos = griddyPos - 1;

            arrayRef.Add(referenceArray);

            //if (griddyInput[griddyPos] == 1)
            //    spockAmount++;

            posX++;
            griddyPos++;
        }
        Debug.Log("Input: \n" + spockLayout[2, 0] + ", " + spockLayout[2, 1] + ", " + spockLayout[2, 2] + "\n" + spockLayout[1, 0] + ", " + spockLayout[1, 1] + ", " + spockLayout[1, 2] + "\n" + spockLayout[0, 0] + ", " + spockLayout[0, 1] + ", " + spockLayout[0, 2]);

        var connectionChecks = new SpockConnections[3, 3];

        var checkIndex = 0;
        var checkX = 0;
        var checkY = 0;

        while (checkIndex < connectionChecks.Length)
        {
            if (checkX == 3)
            {
                checkX = 0;
                checkY++;
            }

            if (spockLayout[checkX, checkY] != 0)
            {
                connectionChecks[checkX, checkY].exists = true;
                if (checkX - 1 >= 0)
                {
                    if (spockLayout[checkX - 1, checkY] == 1)
                        connectionChecks[checkX, checkY].west = true;

                }
                if (checkX + 1 < 3)
                {
                    if (spockLayout[checkX + 1, checkY] == 1)
                        connectionChecks[checkX, checkY].east = true;

                }

                if (checkY - 1 >= 0)
                {
                    if (spockLayout[checkX, checkY - 1] == 1)
                        connectionChecks[checkX, checkY].south = true;
                }
                if (checkY + 1 < 3)
                {
                    if (spockLayout[checkX, checkY + 1] == 1)
                        connectionChecks[checkX, checkY].north = true;
                }
            }
            checkX++;

            checkIndex++;
        }
        Debug.Log("Connections: \n" + connectionChecks[2, 0] + ", " + connectionChecks[2, 1] + ", " + connectionChecks[2, 2] + "\n" 
            + connectionChecks[1, 0] + ", " + connectionChecks[1, 1] + ", " + connectionChecks[1, 2] + "\n" 
            + connectionChecks[0, 0] + ", " + connectionChecks[0, 1] + ", " + connectionChecks[0, 2]);
        return connectionChecks;
    }
    public List<int[]> FindParts(SpockConnections[,] connectionArray)
    {
        var totalPieces = new List<int[]>();
        var posX = 0;
        var posY = 0;

        var currentSpocks = new int[9];

        var checkedSpocks = new bool[3,3];

        var index = 0;

        while (index < currentSpocks.Length)
        {
            if (index == 3 || index == 6)
            {
                posX = 0;
                posY++;
            }

            checkedSpocks[posX, posY] = true;

            if (connectionArray[posX, posY].exists)
            {

                if (connectionArray[posX, posY].north)
                {

                }
            }
            
            posX++;
            index++;
        }

        return totalPieces;
    }
}
