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

    public ParticleSystem deathCubert;
    public ParticleSystem respawnCubert;
    public ParticleSystem deathHopper;
    public ParticleSystem respawnHopper;

    public float maxPlayerSpeed;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(TeleportDelay());
            deathHopper.Play();
            deathCubert.Play();
            respawnCubert.Play();
            respawnHopper.Play();
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxPlayerSpeed);
        if (transform.position.y < -100)
            TeleportPlayer();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane") || collision.gameObject.CompareTag("Lava"))
        {
            deathHopper.Play();
            deathCubert.Play();
            respawnCubert.Play();
            respawnHopper.Play();
            StartCoroutine (TeleportDelay());
            Debug.Log("Teleporting");
        }
        else if (collision.gameObject.CompareTag("SpawnSet"))
        {
            //Debug.Log(collision.gameObject);
            pos = collision.gameObject.GetComponent<RespawnPointSet>().newPos;
        }
    }

    private IEnumerator TeleportDelay() //Delay teleport so particles can activate
    {
        yield return new WaitForSeconds((float)0.15);
        TeleportPlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Teleport Plane") || collision.gameObject.CompareTag("Lava"))
        {
            TeleportPlayer();
            Debug.Log("Death respawn");

            //RESTART SCENE 
            //risingLavaObject.transform.localPosition = new Vector3(0, 0, 0);
            //rL.rising = false;
        }
    }
    public void TeleportPlayer()
    {
        transform.position = pos.position;
        rb.velocity = Vector3.zero;
        foreach (GameObject player in playerBits)
        {
            player.transform.position = pos.position;
        }
    }
}
