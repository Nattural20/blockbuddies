using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPos : MonoBehaviour
{
    public Transform pos;
    public QuickSpawn blockPos;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = pos.position;
            Debug.Log("Moved");
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane"))
        {
            transform.position = pos.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Teleporting");
        }
        else if (collision.gameObject.CompareTag("SpawnSet"))
        {
            Debug.Log(collision.gameObject);
            pos = collision.gameObject.GetComponent<RespawnPointSet>().newPos;
        }
    }
}
