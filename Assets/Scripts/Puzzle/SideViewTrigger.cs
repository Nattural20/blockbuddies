using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SideViewTrigger : MonoBehaviour
{
    public SpawnerCodeV3 arduinoSpawner;
    public LayerMask playerMask;
    public GameObject freeLookCam, sideViewCam;
    CinemachineFreeLook machineFree;
    CinemachineVirtualCamera machineSide;
    public GameObject[] activateObjs;

    int triggersActive;
    bool playerPresent = false;
    private void Start()
    {
        machineFree = freeLookCam.GetComponent<CinemachineFreeLook>();
        machineSide = sideViewCam.GetComponent<CinemachineVirtualCamera>();
    }
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

            if (activateObjs != null)
            {
                foreach (GameObject obj in activateObjs)
                {
                    obj.SetActive(true);
                }
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

                machineFree.m_XAxis.Value = 90;
                sideViewCam.SetActive(false);
                freeLookCam.SetActive(true);
                arduinoSpawner.canSpawnSpocks = true;

                if (activateObjs != null)
                {
                    foreach (GameObject obj in activateObjs)
                    {
                        obj.SetActive(true);
                    }
                }
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
