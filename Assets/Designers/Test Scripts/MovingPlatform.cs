using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public float acceleration;

    public bool moving = true;
    public float maxSpeed;
    private Rigidbody rb;
    public Vector3 distDif;

    public Vector3 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = position1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            var lerpy = Vector3.Lerp(transform.position, targetPos, acceleration * Time.deltaTime);
            var dist = lerpy - transform.position;

            if (dist.magnitude < 0.2)
            {
                if (targetPos == position1.position)
                {
                    StartCoroutine(ChangeDirection(true));
                }
                else
                {
                    StartCoroutine(ChangeDirection(false));
                }
            }
            else
            {
                dist = Vector3.ClampMagnitude(dist, maxSpeed);
                rb.AddForce((dist), ForceMode.Acceleration);
                //rb.velocity = dist;
                distDif = dist;
            }
        }
    }
    public IEnumerator ChangeDirection(bool direction)
    {
        moving = false;
        rb.velocity = Vector3.zero;
        if (direction) targetPos = position2.position;
        else targetPos = position1.position;
        yield return new WaitForSeconds(2);
        moving = true;
    }
}
