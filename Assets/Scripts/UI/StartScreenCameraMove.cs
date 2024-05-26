using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenCameraMove : MonoBehaviour
{
    public GameObject playerCam, startCam;
    public SpawnerSpoofUnspoof spawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            startCam.SetActive(false);
            playerCam.SetActive(true);
        }
    }
    IEnumerator TurnOnSpocks()
    {
        yield return new WaitForSeconds(1);
        spawner.canSpawnSpocks = true;
    }
}
