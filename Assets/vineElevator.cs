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

    public bool rising;
    public float elevatorSpeed = 5;

    private GameObject grabbedSpock;

    // Start is called before the first frame update
    void Start()
    {
        movingGroup = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //foreach (var obj in movingGroup)
        //{
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
        }
        if (grabbedSpock != null)
        {
            grabbedSpock.transform.position = topOfObject.transform.position;
            grabbedSpock.transform.rotation = Quaternion.Slerp(grabbedSpock.transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), 0.2f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Body"))
        {
            collision.transform.GetComponent<SetPos>().TeleportPlayer();
        }

        if (collision.gameObject.CompareTag("Lava Spock"))
        {
            if (grabbedSpock == null)
            {
                grabbedSpock = collision.gameObject;
            }
            if (collision.gameObject.GetComponent<LavaSpockScript>() != null && !collision.gameObject.TryGetComponent<MoveWithElevator>(out MoveWithElevator elevator))
            {
                Destroy(collision.gameObject.GetComponent<LavaSpockScript>());

                //collision.gameObject.AddComponent<MoveWithElevator>();
                //collision.gameObject.GetComponent<MoveWithElevator>().vE = this;

            }
        }
    }

}
