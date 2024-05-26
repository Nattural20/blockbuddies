using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenCameraMove : MonoBehaviour
{
    public GameObject playerCam, startCam;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            startCam.SetActive(false);
            playerCam.SetActive(true);
        }
    }
}
