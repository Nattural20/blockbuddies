using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    public SetPos respawns;
    public PlayerHelperScripts helpScript;
    public int deathLimit = 15;
    bool helpOffered;

    bool counting;
    public int respawnsOnEnter, respawnCount;
    void Update()
    {
        if (!helpOffered)
        {
            if (counting)
            {
                respawnCount = respawns.respawnCounter - respawnsOnEnter;

                if (respawnCount > deathLimit)
                {
                    helpScript.OfferHelp();
                    helpScript.helpOffered = true;
                    helpOffered = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            helpScript.helpActive = true;
            if (!helpOffered)
            {
                if (respawnsOnEnter == 0)
                    respawnsOnEnter = respawns.respawnCounter;
                counting = true;
            }
            else
            {
                helpScript.IndicatorOnly();
                helpOffered = true;
            }
            if (helpScript.helpFunctionOn)
                helpScript.IndicatorOn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            helpScript.helpActive = false;
            helpScript.CutSafetyLine();
            counting = false;
        }
    }
}
