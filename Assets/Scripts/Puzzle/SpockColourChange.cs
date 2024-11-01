using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockColourChange : MonoBehaviour
{
    public MeshRenderer blueSpock, redSpock;
    bool isRed;
    private void Start()
    {
        blueSpock = GetComponent<MeshRenderer>();
    }
    public void ChangeToRed()
    {
        if (!isRed)
        {
            blueSpock.enabled = false;
            redSpock.enabled = true;
            isRed = true;
        }
    }
    public void ChangeToBlue()
    {
        if (isRed)
        {
            redSpock.enabled = false;
            blueSpock.enabled = true;
            isRed = false;
        }
    }
}
