using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewTrigger : MonoBehaviour
{
    public SpawnerCodeV3 arduinoSpawner;
    public LayerMask playerMask;
    public GameObject freeLookCam, sideViewCam;

    int triggersActive;
    bool playerPresent = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            playerPresent = true;
            triggersActive++;


            freeLookCam.SetActive(false);
            sideViewCam.SetActive(true);
            arduinoSpawner.canSpawnSpocks = false;
            foreach (var ghost in arduinoSpawner.ghostSpocks)
            {
                ghost.SetActive(false);
            }

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            triggersActive--;

            if (triggersActive == 0)
            {
                playerPresent = false;
                sideViewCam.SetActive(false);
                freeLookCam.SetActive(true);
                arduinoSpawner.canSpawnSpocks = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Body")
        {
            if (triggersActive == 0)
            {
                playerPresent = true;
                triggersActive++;
    
    
                freeLookCam.SetActive(false);
                sideViewCam.SetActive(true);
                arduinoSpawner.canSpawnSpocks = false;
                foreach (var ghost in arduinoSpawner.ghostSpocks)
                {
                    ghost.SetActive(false);
                }
            }
        }
    }
}
