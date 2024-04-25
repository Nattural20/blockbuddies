using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCheck : MonoBehaviour
{
    public PlayerMovement pM;
    public GameObject relatedArm;
    public bool isHoldingGrabButton, canGrabObject, isGrabbingObject = false;

    private Rigidbody spockRb;

    // Update is called once per frame
    void Update()
    {
        //check if holding grab button
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            isHoldingGrabButton = true;
        }
        else if (Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            isHoldingGrabButton = false;
        }



        //check if you can grab AND if you are holding button
        if (isHoldingGrabButton && canGrabObject && !isGrabbingObject)
        {
            isGrabbingObject = true;
            //FixedJoint fj = relatedArm.AddComponent<FixedJoint>();
            spockRb = pM.currentBlock.GetComponent<Rigidbody>();
            spockRb.excludeLayers = 1 << 7;
            //fj.connectedBody = spockRb;
        }

        if (isGrabbingObject && (!isHoldingGrabButton )) //|| !canGrabObject))
        {
            isGrabbingObject = false;
            //pM.currentBlock.GetComponent<Rigidbody>().excludeLayers = 0 << 7;
            //Destroy(relatedArm.GetComponent<FixedJoint>());
            spockRb.excludeLayers = 0 << 7;
            spockRb = null;
            pM.currentBlock = null;
        }

        if (isGrabbingObject && spockRb != null)
        {
            spockRb.velocity = (transform.position - spockRb.transform.position) * 10;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Spocks")

        {

            canGrabObject = true;

            pM.currentBlock = other.gameObject;


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Spocks")

        {

            canGrabObject = false;

            //pM.currentBlock = null;

        }
    }
}
