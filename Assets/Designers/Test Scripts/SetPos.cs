using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour
{
    public Transform pos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = pos.position;
            Debug.Log("Moved");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Telport Plane"))
        {
            transform.position = pos.position;
        }
        else if (collision.gameObject.CompareTag("SpawnSet"))
        {
            pos = gameObject.GetComponent<RespawnPointSet>().newPos;
        }
    }
}
