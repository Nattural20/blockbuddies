using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartControl : MonoBehaviour
{
    public float cartSpeed = 5f;

    Vector3 target;

    bool goMove, isMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetBusyMoving();

        if (goMove)
        {
            var newPos = Vector3.MoveTowards(transform.position, target, cartSpeed * Time.deltaTime);
            var changeInPos = newPos - transform.position;

            //Debug.Log(changeInPos.magnitude);
            //transform.position = Vector3.Lerp(transform.position, target, cartSpeed * Time.deltaTime);
            if (changeInPos.magnitude > 0.001)
            {
                transform.position = newPos;
            }
            else
            {
                transform.position = target;
                goMove = false;
                isMoving = false;
            }
        }
    }
    public void Move(int distanceX, int distanceZ)
    {
        if (!isMoving)
        {
            goMove = true;
            isMoving = true;

            target = new Vector3(transform.position.x + distanceX * 3, transform.position.y, transform.position.z + distanceZ * 3);
        }
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
