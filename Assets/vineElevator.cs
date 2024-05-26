using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineElevator : MonoBehaviour
{
    public GameObject riseToThisPoint, fallToThisPoint, topOfObject;
    public GameObject elevator;
    public Collider vineElevatorCollider;

    public LavaScript lS;
    public  List<GameObject> movingGroup;

    public bool rising, waiting = true;
    public float elevatorSpeed = 5;

    private GameObject grabbedSpock;

    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        movingGroup = new List<GameObject>();
        startingPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //foreach (var obj in movingGroup)
        //{
        if (!waiting)
        {

            if (rising)
            {
                elevator.transform.Translate(0, elevatorSpeed * Time.fixedDeltaTime, 0);
                /*
                foreach(GameObject item in movingGroup)
                    {
                    item.transform.Translate(0, elevatorSpeed * Time.fixedDeltaTime, 0);
                }
                */
            }
            else
            {
                elevator.transform.Translate(0, -elevatorSpeed * Time.fixedDeltaTime, 0);
            }
        }
        //}
        
    }

    private void Update()
    {
        if (rising && topOfObject.transform.position.y > riseToThisPoint.transform.position.y)
        {
            rising = false;
        }
        else if (!rising && topOfObject.transform.position.y < fallToThisPoint.transform.position.y)
        {
            rising = true;
            Destroy(grabbedSpock);
            grabbedSpock = null;
            waiting = true;
            transform.position = startingPos;
        }
        if (grabbedSpock != null)
        {
            grabbedSpock.transform.position = topOfObject.transform.position;
            grabbedSpock.transform.rotation = Quaternion.Slerp(grabbedSpock.transform.rotation, Quaternion.Euler(0, grabbedSpock.transform.rotation.eulerAngles.y, 0), 0.2f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.transform.GetComponent<SetPos>().TeleportPlayer();
        }

        if (collision.gameObject.CompareTag("Lava Spock") || collision.gameObject.CompareTag("Spocks"))
        {
            if (collision.gameObject.CompareTag("Spocks"))
            {
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            if (grabbedSpock == null)
            {
                grabbedSpock = collision.gameObject;
            }

            if (collision.gameObject.GetComponent<LavaSpockScript>() != null) // && !collision.gameObject.TryGetComponent<MoveWithElevator>(out MoveWithElevator elevator))
            {
                Destroy(collision.gameObject.GetComponent<LavaSpockScript>());
            }

            if (waiting)
            {
                waiting = false;
            }
        }
    }

}
