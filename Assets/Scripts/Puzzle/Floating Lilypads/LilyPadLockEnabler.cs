using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadLockEnabler : MonoBehaviour
{
    public List<LilyPadLockedSpawn> lilyPads;
    public bool playerNearby;
    LilyPadLockedSpawn currentPad;
    void Start()
    {
        //lilyPads = new List<LilyPadLockedSpawn>();
    }
    void FixedUpdate()
    {
        if (playerNearby)
        {
            int ind = 0;
            foreach (var pad in lilyPads)
            {
                if (pad.onScreen == true)
                {
                    if (currentPad == null)
                    {
                        currentPad = pad;
                    }
                    else
                    {
                        if (pad.padDistance < currentPad.padDistance)
                        {
                            currentPad._playerPresent = false;
                            currentPad.BustGhosts();
                            currentPad = pad;
                        }
                    }
                }
                else
                {
                    pad._playerPresent = false;
                    pad.BustGhosts();
                }
                ind++;
            }
            if (currentPad.onScreen == true)
            {
                currentPad._playerPresent = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerNearby = true;
            other.GetComponent<SpawnerCodeV3>().canSpawnSpocks = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerNearby = false;
            other.GetComponent<SpawnerCodeV3>().canSpawnSpocks = true;
        }
    }
}
