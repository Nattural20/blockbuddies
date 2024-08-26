using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewTrigger : MonoBehaviour
{
    public SpawnerCode arduinoSpawner;
    public LayerMask playerMask;
    public GameObject freeLookCam, sideViewCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
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
            sideViewCam.SetActive(false);
            freeLookCam.SetActive(true);
            arduinoSpawner.canSpawnSpocks = true;
        }

    }
}
