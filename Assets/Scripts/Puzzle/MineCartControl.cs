using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartControl : MonoBehaviour
{
    public float cartSpeed = 5f;

    Vector3 target, speedDirection;

    int distance = 1;

    Rigidbody rb;

    bool goMove, isMoving;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetBusyMoving();
        ChangeDistance();

        if (goMove)
            MoveCart();
    }
    void MoveCart()
    {
        var newPos = Vector3.MoveTowards(transform.position, target, cartSpeed * Time.deltaTime);
        var changeInPos = newPos - transform.position;

        if (changeInPos.magnitude > 0.001)
        {
            rb.velocity = speedDirection * cartSpeed;
        }
        else
        {
            transform.position = target;

            rb.constraints = RigidbodyConstraints.FreezeAll;
            speedDirection = Vector3.zero;

            goMove = false;
            isMoving = false;
        }
    }

    public void Move(int distanceX, int distanceZ)
    {
        if (!isMoving)
        {
            goMove = true;
            isMoving = true;

            if (distanceX != 0)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                speedDirection = Vector3.right * distanceX;
            }
            else
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
                speedDirection = Vector3.forward * distanceZ;
            }

            target = new Vector3(transform.position.x + distanceX * 3 * distance, transform.position.y, transform.position.z + distanceZ * 3 * distance);
        }
    }

    void ChangeDistance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            distance = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            distance = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            distance = 3;
    }

    void GetBusyMoving()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }
    }
}
