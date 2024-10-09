using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;

public class PlayerHelperScripts : MonoBehaviour
{
    public bool helpActive, helpOffered;

    public GameObject indicatorUI;
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

        indicatorUI.SetActive(true);
    }
}
