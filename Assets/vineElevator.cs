using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineElevator : MonoBehaviour
{
    public GameObject riseToThisPoint, fallToThisPoint, topOfObject;
    public GameObject elevator;
    public Collider vineElevatorCollider;

    public LavaScript lS;
    private List<GameObject> movingGroup;

    public bool rising;
    public float elevatorSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        movingGroup = new List<GameObject>();
        movingGroup.Add(this.transform.parent.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //foreach (var obj in movingGroup)
        //{
            if (rising)
            {
                elevator.transform.Translate(0, elevatorSpeed * Time.fixedDeltaTime, 0);
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
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            //rising = true;
        }
    }
}
