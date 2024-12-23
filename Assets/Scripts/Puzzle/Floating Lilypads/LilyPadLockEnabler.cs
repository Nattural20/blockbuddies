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
    Vector2 screebSize;
    public float screenCentreWeighting, proximityWeighting, closenessProximity;
    LilyPadLockedSpawn currentPad;

    CubertMovement cM;


    void Start()
    {
        cM = GameObject.Find("CUBERT").GetComponent<CubertMovement>();

        screebSize = new Vector2(Screen.width, Screen.height);
        screenCentreWeighting = (screenCentreWeighting/1920) * screebSize.x;

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
                            if (currentPad.centerDistance < screenCentreWeighting)
                            {
                                if (pad.screenTop && !currentPad.screenTop)
                                {
                                    if (closenessProximity < pad.padDistance && pad.padDistance < proximityWeighting)
                                    {
                                        currentPad._playerPresent = false;
                                        currentPad.BustGhosts();
                                        currentPad = pad;
                                    }
                                }
                            }
                            else if (closenessProximity < pad.padDistance && pad.padDistance < proximityWeighting)
                            {
                                currentPad._playerPresent = false;
                                currentPad.BustGhosts();
                                currentPad = pad;
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
            if (currentPad != null)
            {
                cM.cubertOnLock = true;
                cM.currentSpawnLockPosition = new Vector3(currentPad.transform.position.x, currentPad.transform.position.y +3, currentPad.transform.position.z);


                if (currentPad.onScreen == true)
                {
                    currentPad._playerPresent = true;
                }
                else
                {
                    cM.cubertOnLock = false;
                    currentPad.BustGhosts();
                    currentPad._playerPresent = false;
                    currentPad = null;
                }
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (!playerNearby)
            {
                playerNearby = true;
                spawner.canSpawnSpocks = false;
                foreach (GameObject ghost in spawner.ghostSpocks)
                {
                    ghost.SetActive(false);
                }
            }
        }
    }
}
