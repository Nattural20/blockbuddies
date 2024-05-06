using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float camSpeed = 100f;

    private Vector3 lookInputs;
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, camSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, camSpeed * Time.deltaTime, 0));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        lookInputs.y += Input.GetAxis("Mouse X");
        lookInputs.z += Input.GetAxis("Mouse Y");
        
        lookInputs.z = Mathf.Clamp(lookInputs.z, -89, 10);
        transform.rotation = Quaternion.Euler(lookInputs);
    }
}
