using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handCheck : MonoBehaviour
{
    public PlayerMovement pM;
    public GameObject relatedArm;
    public bool isHoldingGrabButton, canGrabObject, isGrabbingObject = false;



    // Update is called once per frame
    void Update()
    {
        //check if holding grab button
        if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            isHoldingGrabButton = true;
        }
        else if (Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.JoystickButton2))
        {
            isHoldingGrabButton = false;
        }



        //check if you can grab AND if you are holding button
        if (isHoldingGrabButton && canGrabObject && !isGrabbingObject)
        {
            isGrabbingObject = true;
            FixedJoint fj = relatedArm.AddComponent<FixedJoint>();
            fj.connectedBody = pM.currentBlock.gameObject.GetComponent<Rigidbody>();
        }

        if (isGrabbingObject && (!isHoldingGrabButton || !canGrabObject))
        {
            isGrabbingObject = false;
            Destroy(relatedArm.GetComponent<FixedJoint>());
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

            pM.currentBlock = null;

        }
    }
}
