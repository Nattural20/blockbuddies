using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockSpawnPlayerDetector : MonoBehaviour
{
    public bool playerPresent;
    public Material[] materials;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerPresent = true;
            GetComponent<MeshRenderer>().material = materials[1];
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            playerPresent = false;
            GetComponent<MeshRenderer>().material = materials[0];
        }
    }
}
