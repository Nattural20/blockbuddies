using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpockScript : MonoBehaviour
{
    public float sinkSpeed, sinkRotationSpeed;
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;

    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (sinkSpeed * Time.deltaTime), transform.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), sinkRotationSpeed * Time.deltaTime);
    }
}
