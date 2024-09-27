using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LockZoneMovement : MonoBehaviour
{
    public float speed = 1;
    Rigidbody rb;
    Vector3 startPos;
    public PlayerController controller;

    public int playerPresent;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetLilyZonePosition(startPos);
        }
    }
    public void ResetLilyZonePosition(Vector3 ResetPos)
    {
        transform.position = ResetPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pad Reset"))
        {
            GetComponent<LilyPadLockedSpawn>().BustSpocks();
            transform.position -= new Vector3(30, 0, 0); 
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (playerPresent != 0)
            {
                if (controller != null)
                {
                    controller.extraVelocity += new Vector3(speed, 0, 0);
                }
            }
        }
    }
}
