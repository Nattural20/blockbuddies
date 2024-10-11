using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LockZoneMovement : MonoBehaviour
{
    public bool isStatic = false;
    public float speed = 1;
    Rigidbody rb;
    Vector3 startPos;
    public PlayerController controller;

    LilyPadLockedSpawn lockSpawn;

    public int playerPresent;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        lockSpawn = GetComponentInParent<LilyPadLockedSpawn>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetLilyZonePosition(startPos);
        }
    }
    public void ResetLilyZonePosition(Vector3 ResetPos)
    {
        transform.position = ResetPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pad Reset"))
        {
            bool spockActive = false;
            foreach (LockZoneMovingSpocks egg in lockSpawn.spawnCubesParent.GetComponentsInChildren<LockZoneMovingSpocks>())
            {
                if (egg.gameObject.activeSelf)
                    spockActive = true;
            }

            if (spockActive)
            {
                SinkSpock(other.gameObject);
                //var leftoverCubes = Instantiate(lockSpawn.spawnCubesParent, lockSpawn.spawnCubesParent.transform.position, lockSpawn.spawnCubesParent.transform.rotation);
                //leftoverCubes.transform.localScale = lockSpawn.spawnCubesParent.transform.lossyScale;
                //Destroy(leftoverCubes.GetComponent<RotationalBehaviourYAxis>());
                //var waterfallSpock = leftoverCubes.AddComponent<SpockOverWaterfall>();
                //waterfallSpock.speed = speed;
                //waterfallSpock.MoveDelay(other.GetComponent<LilyPadResetValue>().DelayOrNot());
            }
            //other.GetComponent<LilyPadResetValue>().LeftoverSpocks(leftoverCubes);

            GetComponent<LilyPadLockedSpawn>().BustSpocks();
            
            transform.position -= new Vector3(other.GetComponent<LilyPadResetValue>().resetDistance, 0, 0);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isStatic)
        {
            if (other.gameObject.CompareTag("Body"))
            {
                if (playerPresent != 0)
                {
                    if (controller != null)
                    {
                        controller.extraVelocity += new Vector3(speed, 0, 0);
                    }
                }
            }
        }
    }
    void SinkSpock(GameObject ResetPad)
    {
        var leftoverCubes = Instantiate(lockSpawn.spawnCubesParent, lockSpawn.spawnCubesParent.transform.position, lockSpawn.spawnCubesParent.transform.rotation);
        leftoverCubes.transform.localScale = lockSpawn.spawnCubesParent.transform.lossyScale;
        Destroy(leftoverCubes.GetComponent<RotationalBehaviourYAxis>());
        var waterfallSpock = leftoverCubes.AddComponent<SpockOverWaterfall>();
        waterfallSpock.speed = speed;
        waterfallSpock.MoveDelay(ResetPad.GetComponent<LilyPadResetValue>().DelayOrNot());
    }
}
