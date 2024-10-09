using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHelpRiver : PlayerHelperScripts
{
    public LilyPadLockEnabler lilyPads;
    public Image indicatorColour;

    Color defaultCol, onCol;

    public float defaultSpeed;
    public float newSpeed;

    public bool slowedRiver = false;

    public bool resetD = true;
    private void Start()
    {
        defaultCol = new Color(indicatorColour.color.r, indicatorColour.color.g, indicatorColour.color.b, indicatorColour.color.a);
        onCol = new Color(defaultCol.r, Color.green.g, defaultCol.b, defaultCol.a);
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
                        if (!slowedRiver)
                            SlowRiver();
                        else
                            SpeedUpRiver();
                        resetD = false;
                    }
                }
                else if (Input.GetAxis("DPadVertical") == 0)
                    resetD = true;
            }

            else
            {
                if (resetD)
                {
                    if (Input.GetAxis("DualPadVertical") > 0) // DPad Up
                    {
                        if (!slowedRiver)
                            SlowRiver();
                        else
                            SpeedUpRiver();
                        resetD = false;
                    }
                }
                else if (Input.GetAxis("DualPadVertical") == 0)
                    resetD = true;
            }
        }
    }

    public void SlowRiver()
    {
        if (!helpOffered)
        {
            indicatorUI.SetActive(true);
        }
        foreach (LilyPadLockedSpawn movingLily in lilyPads.lilyPads)
        {
            movingLily.GetComponent<LockZoneMovement>().speed = newSpeed;
        }
        indicatorColour.color = onCol;
        slowedRiver = true;
    }
    public void SpeedUpRiver()
    {
        if (!helpOffered)
        {
            indicatorUI.SetActive(false);
        }
        foreach (LilyPadLockedSpawn movingLily in lilyPads.lilyPads)
        {
            movingLily.GetComponent<LockZoneMovement>().speed = defaultSpeed;
        }
        indicatorColour.color = defaultCol;
        slowedRiver = false;
    }
}
