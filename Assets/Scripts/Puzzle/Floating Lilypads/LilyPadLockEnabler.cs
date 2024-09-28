using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LilyPadLockEnabler : MonoBehaviour
{
    public SpawnerCodeV3 spawner;
    public bool playerNearby;
    //public List<LilyPadLockedSpawn> lilyPads;
    public GameObject lilyPadParent;
    public LilyPadLockedSpawn[] lilyPads;
    public float centerWeighting, proximityWeighting;
    LilyPadLockedSpawn currentPad;
    void Start()
    {
        //lilyPads = new List<LilyPadLockedSpawn>();
        lilyPads = lilyPadParent.GetComponentsInChildren<LilyPadLockedSpawn>();
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
                        if (pad.centerDistance < currentPad.centerDistance)
                        {
                            if (5 < pad.padDistance && pad.padDistance < proximityWeighting)
                            {
                                currentPad._playerPresent = false;
                                currentPad.BustGhosts();
                                currentPad = pad;
                                //Debug.Log("The active pad is now " + currentPad.name + " with an offset of " + currentPad.centerDistance);
                            }
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
            else
            {
                currentPad.BustGhosts();
                currentPad._playerPresent = false;
                currentPad = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerNearby = true;
            spawner.canSpawnSpocks = false;
            foreach (GameObject ghost in spawner.ghostSpocks)
            {
                ghost.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerNearby = false;
            spawner.GetComponent<SpawnerCodeV3>().canSpawnSpocks = true;
        }
    }
}
