using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadResetValue : MonoBehaviour
{
    public float resetDistance;
    public bool turnSpocks;
    bool delayLerpSwapper = true;
    public bool DelayOrNot()
    {
        if (turnSpocks)
        {
            delayLerpSwapper = !delayLerpSwapper;
            return delayLerpSwapper;
        }
        else
            return true;
    }
}
