using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraFocusPull : MonoBehaviour
{
    public CinemachineFreeLook machine;
    Vector2 defaultSpeed;

    CinemachineFreeLook newCam;

    public Transform newLookAt;

    public float lookLength = 5;
    Transform oldLookAt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            GetComponent<Collider>().enabled = false;
            defaultSpeed = new Vector2 (machine.m_XAxis.m_MaxSpeed, machine.m_YAxis.m_MaxSpeed);
            machine.m_XAxis.m_MaxSpeed = 0;
            machine.m_YAxis.m_MaxSpeed = 0;

            newCam = Instantiate(machine);
            machine.gameObject.SetActive(false);
            oldLookAt = machine.LookAt;
            newCam.LookAt = newLookAt;

            newCam.m_XAxis.m_MaxSpeed = 0;
            newCam.m_YAxis.m_MaxSpeed = 0;
            newCam.m_YAxis.Value = 1;
            newCam.gameObject.GetComponent<CinemachineCollider>().enabled = false;

            StartCoroutine(StopLook());
        }
    }
    IEnumerator StopLook()
    {
        yield return new WaitForSeconds(lookLength);
        newCam.gameObject.SetActive(false);
        machine.gameObject.SetActive(true);
        machine.m_XAxis.m_MaxSpeed = defaultSpeed.x;
        machine.m_YAxis.m_MaxSpeed = defaultSpeed.y;
        yield return new WaitForSeconds(5);
        Destroy(newCam.gameObject);
        Destroy(this);
    }
}
