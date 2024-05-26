using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockSpawnPlayerDetector : MonoBehaviour
{
    public bool playerPresent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerPresent = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerPresent = false;
        }
    }
}
