using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZoneMovingSpocks : MonoBehaviour
{
    LockZoneMovement lockZone;
    LilyLookSelection lookSelection;
    public bool present, detected;
    private void Start()
    {
        lockZone = GetComponentInParent<LockZoneMovement>();
        lookSelection = GetComponentInChildren<LilyLookSelection>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            lockZone.playerPresent++;
            if (lockZone.controller == null)
            {
                lockZone.controller = collision.gameObject.GetComponentInParent<PlayerController>();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            lockZone.playerPresent--;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            detected = true;
        }
    }
    private void Update()
    {
        present = false;
    }
}
