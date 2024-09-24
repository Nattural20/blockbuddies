using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class RiverSpockScript : MonoBehaviour
{
    public Vector3 pushDirection, aimDirection;
    public Rigidbody rb;
    public float speed;
    public float aimSpeed, lerpSpeed;
    public bool gotDirection, gotNewDirection;
    PlayerController controller;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void FixedUpdate()
    {
        if (gotNewDirection)
        {
            //pushDirection = Vector3.Lerp(pushDirection, aimDirection, lerpSpeed * Time.deltaTime);
            //speed = Mathf.Lerp(speed, aimSpeed, lerpSpeed * Time.deltaTime);

        }
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
    public void NewPushDirection(Vector3 newDirection, float newSpeed, float changeInLerp)
    {
        aimDirection = newDirection;
        aimSpeed = newSpeed;
        lerpSpeed = changeInLerp;
        gotNewDirection = true;
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
