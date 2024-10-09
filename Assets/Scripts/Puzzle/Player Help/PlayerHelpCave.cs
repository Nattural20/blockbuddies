using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHelpCave : PlayerHelperScripts
{
    public GameObject[] thorns;

    bool shrunkThorns = false;
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
                        if (!shrunkThorns)
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
                        if (!shrunkThorns)
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
            indicatorUI.SetActive(true);
        }

        foreach (GameObject thorn in thorns)
        {
            thorn.SetActive(false);
        }

        IndicatorOn();
        shrunkThorns = true;
    }
    public void BiggenThorns()
    {
        if (!helpOffered)
            indicatorUI.SetActive(false);

        foreach (GameObject thorn in thorns)
            thorn.SetActive(true);

        IndicatorOff();
        shrunkThorns = false;
    }
}
