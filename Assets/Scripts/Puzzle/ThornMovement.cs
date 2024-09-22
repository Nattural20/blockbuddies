using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMovement : MonoBehaviour
{
    public float speed, distance;

    Vector3 leftEnd, rightEnd;
    bool goingLeft = true;
    void Start()
    {
        leftEnd = transform.position + transform.right * -1 * distance;
        rightEnd = transform.position + transform.right * distance;
    }
    void Update()
    {
        if (goingLeft)
        {
            if ((transform.position - Vector3.MoveTowards(transform.position, leftEnd, speed / 10)).magnitude > 0.01)
                transform.position = Vector3.MoveTowards(transform.position, leftEnd, speed / 10);
            else
                goingLeft = false;
        }
        else
        {
            if ((transform.position - Vector3.MoveTowards(transform.position, rightEnd, speed/10)).magnitude > 0.01)
                transform.position = Vector3.MoveTowards(transform.position, rightEnd, speed/10);
            else
                goingLeft = true;
        }
    }
}
