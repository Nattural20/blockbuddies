using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSpockScript : MonoBehaviour
{
    public float sinkSpeed, sinkRotationSpeed;
    public bool upsideDown = false;
    void Start()
    {
        GetComponent<Rigidbody>().isKinematic = true;

        //if (Vector3.Angle(transform.up, Vector3.up) > 90)
        //{
        //    upsideDown = true;
        //}
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (sinkSpeed * Time.deltaTime), transform.position.z);

        if (!upsideDown)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), sinkRotationSpeed * Time.deltaTime);
            var newAngle = Vector3.Lerp(transform.rotation.eulerAngles, Vector3.up, 1);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newAngle.x, transform.rotation.eulerAngles.y, newAngle.z), sinkRotationSpeed * Time.deltaTime);
        }
        else
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 180), sinkRotationSpeed * Time.deltaTime);
            //var newAngle = Vector3.Lerp(transform.rotation.eulerAngles, Vector3.down, 1);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 180), sinkRotationSpeed * Time.deltaTime);
        }
    }
}
