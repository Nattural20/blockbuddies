using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewTrigger : MonoBehaviour
{
    public SpawnerCodeV3 arduinoSpawner;
    public LayerMask playerMask;
    public GameObject freeLookCam, sideViewCam;

    int triggersActive;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
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
                sideViewCam.SetActive(false);
                freeLookCam.SetActive(true);
                arduinoSpawner.canSpawnSpocks = true;
            }
        }

    }
}
