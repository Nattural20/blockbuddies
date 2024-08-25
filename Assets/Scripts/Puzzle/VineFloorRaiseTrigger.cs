using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineFloorRaiseTrigger : MonoBehaviour
{
    private VineFloorRaise raiseFloor;
    private void Start()
    {
        raiseFloor = GetComponentInParent<VineFloorRaise>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            raiseFloor.RaiseFloor();
        }
    }
}
