using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public Transform spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleport Plane") || other.gameObject.CompareTag("Lava"))
        {
            transform.position = spawnLocation.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane") || collision.gameObject.CompareTag("Lava"))
        {
            transform.position = spawnLocation.position;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
