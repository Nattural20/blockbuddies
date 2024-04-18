using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Teleport Plane"))
        {
            Destroy(gameObject);
        }
    }
}
