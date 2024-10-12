using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LilyPadsDestroy : MonoBehaviour
{
    LilyPadSuckedDown[] padList;
    public void ActivateLilyDestroy(float waitTime)
    {
        StartCoroutine(DestroyLilies(waitTime));
    }
    IEnumerator DestroyLilies(float waitTime)
    {
        padList = GetComponentsInChildren<LilyPadSuckedDown>();

        PadActivate(waitTime, 0);
        PadActivate(waitTime, 1);
        PadActivate(waitTime, 2);

        yield return new WaitForSeconds(waitTime / 3);
        PadActivate(waitTime, 3);
        PadActivate(waitTime, 4);
        PadActivate(waitTime, 5);
        
        yield return new WaitForSeconds(waitTime / 3);
        PadActivate(waitTime, 6);
        PadActivate(waitTime, 7);
        PadActivate(waitTime, 8);

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
    void PadActivate(float waitTime, int padNo)
    {
        padList[padNo].suckDestroySpeed = waitTime;
        padList[padNo].SuckAndDestroy();
    }
}