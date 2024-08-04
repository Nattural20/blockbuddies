using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftMovement : MonoBehaviour
{
    private bool _playerPresent = false;
    public SpawnerSpoof spawnScript;
    public ArduinoReaderSpoof readerScript; //same as spawnScript
    public GameObject raft; //Reference to object that will be moved
    public float movementSpeed = 10f;
    public float acceleration = 5f; // Acceleration rate
    public float maxVelocity = 20f; // Maximum speed cap
    private float currentVelocity = 0f;
    private float verticalInput = 0f;
    //Should we attach the player to the raft while it moves?
    void Start()
    {
        Debug.Log("Flux Capacitor... Fluxxing...");
        //raft = gameObject;
    }

    void Update()
    {
        if (spawnScript)
        {
            if (_playerPresent)
            {
                MoveRaft();
                spawnScript.enabled = false;
            }
            else
            {
                spawnScript.enabled = true;
            }
        }
        else
        {
            Debug.Log("Spawn Script not given or cannot be read.");
        }
    }

    private void MoveRaft()
    {
        float raftDirection = CheckInput();
        // Update velocity based on input
        if (raftDirection != 0)
        {
            // Accelerate in the direction of the input
            currentVelocity += acceleration * Time.deltaTime;
            // Clamp velocity to maxVelocity
            currentVelocity = Mathf.Clamp(currentVelocity, 0, maxVelocity);
        }
        else
        {
            // Gradually decelerate if there's no input
            currentVelocity = Mathf.Lerp(currentVelocity, 0, acceleration * Time.deltaTime);
        }

        // Move the raft based on current velocity
        raft.transform.position += new Vector3(raftDirection * currentVelocity * Time.deltaTime, verticalInput * currentVelocity * Time.deltaTime, 0);
    }

    private float CheckInput()
    {
        int[] outputArray = readerScript.OutputArray;
        float input = 0;
        if (outputArray[1] == 1) input++;
        if (outputArray[4] == 1) input++;
        if (outputArray[7] == 1) input++;
        if (outputArray[3] == 1) input--;
        if (outputArray[6] == 1) input--;
        if (outputArray[9] == 1) input--;
        return input;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Body")
        {
            _playerPresent = false;
        }
    }
}
