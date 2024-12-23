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
            PlayerPresent();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            PlayerNotPresent();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            PlayerPresent();
        }
    }
    public void PlayerPresent()
    {
        playerPresent = true;
        GetComponent<MeshRenderer>().material = materials[1];
    }
    public void PlayerNotPresent()
    {
        playerPresent = false;
        GetComponent<MeshRenderer>().material = materials[0];
    }
}
