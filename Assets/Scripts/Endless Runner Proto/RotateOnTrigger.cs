using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTrigger : MonoBehaviour
{
    public Transform targetObject; // Reference to the object you're checking against
    private float width;
    private Vector3 leftSide;
    private Vector3 rightSide;

    private int side = 0;

    public float rotateSpeed = 2f; // Speed of rotation
    private bool isRotating = false;
    private Quaternion targetRotation;

    private void Start()
    {
        Renderer renderer = targetObject.GetComponent<Renderer>();

        // Calculate the width along the x-axis
        width = renderer.bounds.size.x;

        // Determine the positions of the left and right sides
        leftSide = targetObject.position - new Vector3(width / 2, 0, 0);
        rightSide = targetObject.position + new Vector3(width / 2, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Raft") && !isRotating && side == 0)
        {
            // Define the target rotation (20 degrees around the Y-axis in this example)
            targetRotation = Quaternion.Euler(0, 20, 0);
            isRotating = true;
        }

        else if (other.CompareTag("Raft") && !isRotating && side == 0)
        {
            targetRotation = Quaternion.Euler(0, -20, 0);
            isRotating = true;
        }
    }

    void Update()
    {

        float playerX = transform.position.x;

        // Compare the player's position with the object's sides
        if (Mathf.Abs(playerX - leftSide.x) < Mathf.Abs(playerX - rightSide.x))
        {
            Debug.Log("Closer to the left side.");
            side = 0;
        }
        else
        {
            Debug.Log("Closer to the right side.");
            side = 1;
        }



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
