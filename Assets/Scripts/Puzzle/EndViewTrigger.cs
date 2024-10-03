using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndViewTrigger : MonoBehaviour
{
    public LayerMask playerMask;
    public GameObject freeLookCam, endViewCam;


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            freeLookCam.SetActive(false);
            endViewCam.SetActive(true);
        }
    }

}
