using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFocusPull : MonoBehaviour
{
    public CinemachineFreeLook machine;
    CinemachineFreeLook newCam;

    public Transform newLookAt;

    public float lookLength = 5;
    Transform oldLookAt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            GetComponent<Collider>().enabled = false;

            newCam = Instantiate(machine);
            machine.gameObject.SetActive(false);
            oldLookAt = machine.LookAt;
            newCam.LookAt = newLookAt;

            newCam.m_XAxis.m_MaxSpeed = 0;
            newCam.m_YAxis.m_MaxSpeed = 0;
            newCam.gameObject.GetComponent<CinemachineCollider>().enabled = false;

            StartCoroutine(StopLook());
        }
    }
    IEnumerator StopLook()
    {
        yield return new WaitForSeconds(lookLength);
        newCam.gameObject.SetActive(false);
        machine.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy(newCam.gameObject);
        Destroy(this);
    }
}
