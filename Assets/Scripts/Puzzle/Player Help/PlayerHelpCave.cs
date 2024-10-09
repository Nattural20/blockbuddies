using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHelpCave : PlayerHelperScripts
{
    public GameObject[] thorns;

    bool resetD = true;
    private void Update()
    {
        if (helpActive)
        {
            if (!isDualShock)
            {
                if (resetD)
                {
                    if (Input.GetAxis("DPadVertical") < 0) // DPad Up
                    {
                        if (!helpFunctionOn)
                            ShrinkThorns();
                        else
                            BiggenThorns();
                        resetD = false;
                    }
                }
                else if (Input.GetAxis("DPadVertical") == 0)
                {
                    resetD = true;
                }
            }

            else
            {
                if (resetD)
                {
                    if (Input.GetAxis("DualPadVertical") > 0) // DPad Up
                    {
                        if (!helpFunctionOn)
                            ShrinkThorns();
                        else
                            BiggenThorns();
                        resetD = false;
                    }
                }
                else if (Input.GetAxis("DualPadVertical") == 0)
                {
                    resetD = true;
                }
            }
        }
    }

    public void ShrinkThorns()
    {
        if (!helpOffered)
        {
            indicatorUIParent.SetActive(true);
        }

        foreach (GameObject thorn in thorns)
        {
            thorn.SetActive(false);
        }

        IndicatorOn();
        helpFunctionOn = true;
    }
    public void BiggenThorns()
    {
        if (!helpOffered)
            indicatorUIParent.SetActive(false);

        foreach (GameObject thorn in thorns)
            thorn.SetActive(true);

        IndicatorOff();
        helpFunctionOn = false;
    }
}
