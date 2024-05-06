using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position1;
    Vector3 stopPos1;
    public Transform position2;
    Vector3 stopPos2;
    public float acceleration;
    public float maxSpeed;

    public bool moving = true;
    private Rigidbody rb;
    public Vector3 distDif;

    public Vector3 targetPos;

    Vector3 debugDist;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stopPos1 = position1.position - new Vector3(transform.localScale.x/2, 0, 0);
        stopPos2 = position2.position + new Vector3(transform.localScale.x/2, 0, 0);

        targetPos = stopPos1;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            var lerpy = Vector3.Lerp(transform.position, targetPos, acceleration * Time.deltaTime);
            var dist = lerpy - transform.position;
            //var lerpy = Vector3.Lerp(transform.position, targetPos, maxSpeed * Time.deltaTime);
            //var dist = lerpy - transform.position;
            //var dist = (targetPos - transform.position ) / 2;
            dist = Vector3.ClampMagnitude(dist, maxSpeed);

            if (dist.magnitude < 0.1)
            {
                if (targetPos == stopPos1)
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
                rb.AddForce(dist, ForceMode.Acceleration);
                //rb.velocity = dist;
                distDif = dist;
            //}
            debugDist= dist;
                //dist = Vector3.ClampMagnitude(dist, pullStrength);
                //rb.AddForce((dist * pullStrength), ForceMode.Impulse);
               // rb.velocity = dist * pullStrength;
            }
            Debug.DrawLine(transform.position, transform.position + dist, Color.red);
            Debug.Log(dist);
        }
        else rb.velocity = Vector3.zero;
        //Debug.Log("Dist is " + debugDist + "." + "rb velocity is" + rb.velocity);
    }
    public IEnumerator ChangeDirection(bool direction)
    {
        moving = false;
        rb.velocity = Vector3.zero;
        if (direction) targetPos = stopPos2;
        else targetPos = stopPos1;
        yield return new WaitForSeconds(2);
        moving = true;
    }
}
