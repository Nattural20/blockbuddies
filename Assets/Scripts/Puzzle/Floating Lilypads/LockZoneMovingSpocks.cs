using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockZoneMovingSpocks : MonoBehaviour
{
    LilyPadLockedSpawn lilySpawn;
    LockZoneMovement lockZone;
    LilyLookSelection lookSelection;
    public bool present, detected;
    private void Start()
    {
        lilySpawn = GetComponentInParent<LilyPadLockedSpawn>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lily Pad Sucker"))
        {
            other.GetComponent<VineSuperSucker>().ActivateVineSuck(lilySpawn.spawnCubesParent);
            lilySpawn.BustSpocks();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            lockZone.playerPresent--;
        }
    }
    private void Update()
    {
        detected = false;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            detected = true;
        }
    }
}
