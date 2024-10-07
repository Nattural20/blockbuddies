using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelperScripts : MonoBehaviour
{
    public bool helpActive;

    public GameObject indicatorUI;
    public GameObject[] UIElements;
    public void OfferHelp()
    {
        foreach (GameObject help in UIElements)
        {
            help.SetActive(true);
        }
    }
    public void CutSafetyLine()
    {
        foreach (GameObject help in UIElements)
        {
            help.SetActive(false);
        }
    }
    public void IndicatorOnly()
    {
        indicatorUI.SetActive(true);
    }
}
