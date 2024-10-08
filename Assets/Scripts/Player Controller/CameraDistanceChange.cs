using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraDistanceChange : MonoBehaviour
{
    public CinemachineFreeLook machine;
    public Vector3 newRadii;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            machine.m_Orbits[0].m_Radius = newRadii.x;
            machine.m_Orbits[1].m_Radius = newRadii.y;
            machine.m_Orbits[2].m_Radius = newRadii.z;
        }
    }
}
