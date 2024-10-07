using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelper : MonoBehaviour
{
    public SetPos respawns;
    public PlayerHelperScripts helpScript;
    bool helpOffered;

    bool counting;
    int respawnsOnEnter, respawnCount;
    void Update()
    {
        if (!helpOffered)
        {
            if (counting)
            {
                respawnCount = respawns.respawnCounter - respawnsOnEnter;
                if (respawnCount > 15)
                {
                    helpScript.OfferHelp();
                    helpOffered = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
           respawnsOnEnter = respawns.respawnCounter;
           counting = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            counting = false;
        }
    }
}
