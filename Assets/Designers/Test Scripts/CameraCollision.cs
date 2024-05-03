using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform aim;
    public GameObject cam;
    public GameObject testCube;
    void Update()
    {
        var camRayHit = new Ray(transform.position, aim.position - transform.position);
        if (Physics.Raycast(camRayHit, out RaycastHit hit, 4f, 0 << 7 + 0 << 8))
        {
            cam.transform.position = hit.point;
            cam.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 90, 0);

            testCube.transform.position = hit.point;
        }
        else
        {
            
            cam.transform.position  = camRayHit.GetPoint(4f); //= transform.position - (transform.position - aim.position) * 3.6f;
            cam.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 90, 0);

            testCube.transform.position = transform.position - (aim.position - transform.position);
        }
    }
}
