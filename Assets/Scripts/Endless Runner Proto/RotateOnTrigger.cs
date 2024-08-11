using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTrigger : MonoBehaviour
{
    public float rotateSpeed = 2f; // Speed of rotation
    private bool isRotating = false;
    private Quaternion targetRotation;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft") && !isRotating)
        {
            // Define the target rotation (20 degrees around the Y-axis in this example)
            targetRotation = Quaternion.Euler(0, 20, 0);
            isRotating = true;
        }
    }

    void Update()
    {
        if (isRotating)
        {
            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Stop rotating when close enough to the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }
}
