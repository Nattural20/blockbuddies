using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Xml.Serialization;

public class PlayerHelperScripts : MonoBehaviour
{
    public bool helpActive, helpOffered, helpFunctionOn;

    public GameObject indicatorUIParent;
    public GameObject[] indicatorUI;
    public GameObject[] UIElements;

    public bool isDualShock;
    public void OfferHelp()
    {
        if (Gamepad.current is DualShockGamepad)
            isDualShock = true;
        else
            isDualShock = false;

        foreach (GameObject help in UIElements)
        {
            help.SetActive(true);
        }
    }
    public void CutSafetyLine()
    {
        IndicatorOff();
        foreach (GameObject help in UIElements)
        {
            help.SetActive(false);
        }
    }
    public void IndicatorOnly()
    {
        if (Gamepad.current is DualShockGamepad)
            isDualShock = true;
        else
            isDualShock = false;

        indicatorUIParent.SetActive(true);
    }
    public void IndicatorOn()
    {
        indicatorUI[0].SetActive(false);
        indicatorUI[1].SetActive(true);
        //indicatorColour.color = onCol;
    }
    public void IndicatorOff()
    {
        indicatorUI[1].SetActive(false);
        indicatorUI[0].SetActive(true);
        //indicatorColour.color = defaultCol;
    }
}
