using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldRaftMovement : MonoBehaviour
{
    private bool _playerPresent = false;
    public SpawnerSpoof spawnScript;
    public ArduinoReaderSpoof readerScript; //same as spawnScript
    public GameObject raft; //Reference to object that will be moved
    public float movementSpeed = 10f;
    public float acceleration = 5f; // Acceleration rate
    public float maxVelocity = 20f; // Maximum speed cap
    private float currentVelocity = 0f;
    private float verticalInput = 0f; //dont use this.... dont do it... no....
    public float maxLeft = 50f;
    public float maxRight = -50f;
    [SerializeField]
    private float raftDirection;
    [SerializeField]
    private float currentMovement;
    public float returnLerpSpeed = 2f; //change this to affect how fast the raft moves back to normal position

    //Note that raft is interchangable to whatever gameobject or gameobject parent we are moving
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
        raftDirection = CheckInput();

        if (raftDirection != 0)
        {
            // Increase velocity based on input direction
            currentVelocity += raftDirection * acceleration * Time.deltaTime;
            currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);

            // Calculate the new position based on the velocity
            Vector3 newPosition = raft.transform.position + Vector3.right * currentVelocity * Time.deltaTime;

            // Ensure the new position stays within the left and right boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, maxRight, maxLeft);

            // Apply the movement
            raft.transform.position = newPosition;

            // Update currentMovement to track how far the raft has moved from the starting position
            currentMovement = newPosition.x;
        }
        else
        {
            // Gradually decrease velocity when no input is detected
            currentVelocity = Mathf.Lerp(currentVelocity, 0, acceleration * Time.deltaTime);

            // Smoothly return to the original position
            Vector3 originalPosition = new Vector3(0, raft.transform.position.y, raft.transform.position.z);
            raft.transform.position = Vector3.Lerp(raft.transform.position, originalPosition, returnLerpSpeed * Time.deltaTime);

            // Update currentMovement to track the raft's position relative to the starting point
            currentMovement = raft.transform.position.x;
        }
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
            raftDirection = 0;
            _playerPresent = false;
        }
    }
}
