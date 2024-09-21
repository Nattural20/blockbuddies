using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSpockScript : MonoBehaviour
{
    Vector3 pushDirection;
    Rigidbody rb;
    public float speed;
    bool gotDirection;
    PlayerController controller;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void FixedUpdate()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - (sinkSpeed * Time.deltaTime), transform.position.z);
        if (gotDirection)
        {
            rb.velocity = pushDirection * speed;
        }
    }
    public void GetPushDirection(Vector3 direction, float riverSpeed)
    {
        pushDirection = direction;
        speed = riverSpeed;
        gotDirection = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("River Edge"))
        {
            gotDirection = false;
            rb.constraints = RigidbodyConstraints.None;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            controller = collision.gameObject.GetComponentInParent<PlayerController>();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            controller.extraVelocity += pushDirection * speed;
        }
    }
}
