using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGeyser : MonoBehaviour
{
    public float geyserForce = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spocks") || other.gameObject.CompareTag("Lava Spock") || other.gameObject.CompareTag("Key"))
        {
            var objRb = other.gameObject.GetComponent<Rigidbody>();
            objRb.isKinematic = false;
            objRb.AddForce(new Vector3(0, geyserForce, 0), ForceMode.Impulse);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spocks") || other.gameObject.CompareTag("Lava Spock") || other.gameObject.CompareTag("Key"))
        {
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }
}
