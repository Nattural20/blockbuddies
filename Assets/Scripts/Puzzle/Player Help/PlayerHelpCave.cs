using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHelpCave : PlayerHelperScripts
{
    public Image helpIndicator;
    Color defaultCol, onCol;

    public GameObject[] thorns;

    bool shrunkThorns = false;
    bool resetD = true;
    private void Start()
    {
        defaultCol = new Color(helpIndicator.color.r, helpIndicator.color.g, helpIndicator.color.b, helpIndicator.color.a);
        onCol = new Color(defaultCol.r, Color.green.g, defaultCol.b, defaultCol.a);

        //defaultSize = thorns[0].transform.localScale;
    }

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
                    if (Input.GetAxis("DualPadVertical") < 0) // DPad Up
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

        helpIndicator.color = onCol;
        shrunkThorns = true;
    }
    public void BiggenThorns()
    {
        if (!helpOffered)
            indicatorUI.SetActive(false);

        foreach (GameObject thorn in thorns)
            thorn.SetActive(true);

        helpIndicator.color = defaultCol;
        shrunkThorns = false;
    }
}
