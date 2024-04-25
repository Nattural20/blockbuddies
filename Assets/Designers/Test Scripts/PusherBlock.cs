using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherBlock : MonoBehaviour
{
    public float extension;
    public float speed = 1f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.velocity = new Vector3(0, 0, speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            speed = -speed;
        }
    }
}
