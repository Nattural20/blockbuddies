using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetPos : MonoBehaviour
{
    public Transform pos;
    public QuickSpawn blockPos;

    public GameObject risingLavaObject;
    public risingLava rL;

    public List<GameObject> playerBits;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = pos.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            TeleportPlayer();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane") || collision.gameObject.CompareTag("Lava"))
        {
            transform.position = pos.position;
            TeleportPlayer();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Teleporting");
        }
        else if (collision.gameObject.CompareTag("SpawnSet"))
        {
            Debug.Log(collision.gameObject);
            pos = collision.gameObject.GetComponent<RespawnPointSet>().newPos;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane") || collision.gameObject.CompareTag("Lava"))
        {
            transform.position = pos.position;
            TeleportPlayer();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log("Death respawn");

            //RESTART SCENE 
            //risingLavaObject.transform.localPosition = new Vector3(0, 0, 0);
            //rL.rising = false;
        }
    }
    public void TeleportPlayer()
    {
        foreach (GameObject player in playerBits)
        {
            player.transform.position = pos.position;
        }
    }
}
