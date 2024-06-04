using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateGrab : MonoBehaviour
{
    public FixedJoint fJ;
    public PlayerController pC;

    private bool spockInFront;
    public bool holdingSpock;
    private GameObject currentSpock;

    // Update is called once per frame
    void Update()
    {
        if (spockInFront && pC.isHoldingGrab && !holdingSpock)
        {
            currentSpock.transform.position = new Vector3(currentSpock.transform.position.x, currentSpock.transform.position.y + .5f, currentSpock.transform.position.z);
            holdingSpock = true;
            fJ.connectedBody = currentSpock.GetComponent<Rigidbody>();
        }

        if (!pC.isHoldingGrab && holdingSpock)
        {
            holdingSpock = false;
            fJ.connectedBody = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spocks" || other.gameObject.tag == "Key")

        {
            spockInFront = true;
            currentSpock = other.gameObject;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Spocks" || other.gameObject.tag == "Key")

        {
            spockInFront = false;
            currentSpock = null;

        }
    }
}
