using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHelpRiver : PlayerHelperScripts
{
    public LilyPadLockEnabler lilyPads;

    public float defaultSpeed;
    public float newSpeed;

    public bool resetD = true;
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
                    if (Input.GetAxis("DualPadVertical") != 0) // DPad Up
                    {
                        if (!helpFunctionOn)
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
        IndicatorOn();
        helpFunctionOn = true;
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
        IndicatorOff();
        helpFunctionOn = false;
    }
}
