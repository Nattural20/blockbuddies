using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockOverWaterfall : MonoBehaviour
{
    public float speed;
    public Vector3 moveDirection, moveLerp;
    bool howRigid = false, lerpActive = true;
    public int playerPresent;
    public PlayerController controller;
    Rigidbody rb;
    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.mass = 150;

        var newCollider = gameObject.AddComponent<BoxCollider>();
        newCollider.isTrigger = true;
        newCollider.size = Vector3.one * 6;

        moveDirection = new Vector3(speed*2, 0, 0);
        moveLerp = new Vector3(0, 0, speed * 10);

        foreach (LockZoneMovingSpocks spock in gameObject.GetComponentsInChildren<LockZoneMovingSpocks>())
        {
            spock.gameObject.GetComponent<Animator>().StopPlayback();
            Destroy(spock.gameObject.GetComponent<Animator>());
            Destroy(spock);
        }

    }
    private void FixedUpdate()
    {
        if (lerpActive)
        {
            if (!howRigid)
            {
                moveDirection = Vector3.Lerp(moveDirection, moveLerp, speed / 200);

                transform.position += moveDirection * Time.deltaTime;
            }
            else
            {
                //rb.velocity = moveDirection;
                //rb.velocity = new Vector3(moveDirection.x * 2, -9.8f, moveDirection.z * 2);
            }
        }
        else
        {
            transform.position += moveDirection * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            controller = collision.gameObject.GetComponentInParent<PlayerController>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            if (controller != null)
            {
                if (!howRigid)
                    controller.extraVelocity += moveDirection;
                else
                    controller.extraVelocity += rb.velocity;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Waterfall End"))
        {
            rb.isKinematic = false;
            howRigid = true;
            rb.velocity = moveDirection;

            StartCoroutine(DestroyDelay());
        }
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(15);
        Destroy(gameObject);
    }
    public void MoveDelay(bool doDelay)
    {
        if (doDelay)
        {
            lerpActive = false;
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);
        lerpActive = true;
    }
}
