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
        //Check if holding grab button
        if (Input.GetKeyDown(KeyCode.G))
        {
            isHoldingGrabButton = true;
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            isHoldingGrabButton = false;
        }



        //Check if you can grab and if you are holding button
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
        if (other.gameObject.tag == "Block")

        {

            canGrabObject = true;

            pM.currentBlock = other.gameObject;


        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")

        {

            canGrabObject = false;

            pM.currentBlock = null;

        }
    }
}
