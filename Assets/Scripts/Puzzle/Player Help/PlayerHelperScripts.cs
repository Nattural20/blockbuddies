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

    public Image indicatorColour;
    Color defaultCol, onCol;
    public GameObject indicatorUI;
    public GameObject[] UIElements;

    public bool isDualShock;
    private void Start()
    {
        defaultCol = new Color(indicatorColour.color.r, indicatorColour.color.g, indicatorColour.color.b, indicatorColour.color.a);
        onCol = new Color(defaultCol.r, Color.green.g, defaultCol.b, defaultCol.a);
    }
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

        indicatorUI.SetActive(true);
    }
    public void IndicatorOn()
    {
        indicatorColour.color = onCol;
    }
    public void IndicatorOff()
    {
        indicatorColour.color = defaultCol;
    }
}
