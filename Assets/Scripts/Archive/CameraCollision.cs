using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform aim;
    public GameObject cam;
    public float camDistance = 4f;

    Vector3 camOffset;
    private void Start()
    {
        camOffset = cam.transform.localPosition;
    }
    void Update()
    {
        var camRayHit = new Ray(transform.position, aim.position - transform.position);
        var mask = 1 << 8 | 1 << 7 | 1 << 11;
        mask = ~mask;
        if (Physics.Raycast(camRayHit, out RaycastHit hit, camDistance, mask))
        {
            cam.transform.position = Vector3.Lerp(hit.point, transform.position, 0.5f);
            //cam.transform.localPosition += camOffset;
            cam.transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 90, 0);
        }
        else
        {
            
            cam.transform.position  = camRayHit.GetPoint(camDistance); //= transform.position - (transform.position - aim.position) * 3.6f;
            //cam.transform.localPosition += camOffset;
            cam.transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.z, transform.rotation.eulerAngles.y + 90, 0);
        }
    }
}
