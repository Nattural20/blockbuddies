using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineElevator : MonoBehaviour
{
    public GameObject riseToThisPoint, fallToThisPoint, topOfObject;

    public LavaScript lS;

    private List<GameObject> movingGroup;


    // Start is called before the first frame update
    void Start()
    {
        movingGroup = new List<GameObject>();
        movingGroup.Add(this.transform.parent.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spock.transform.position = new Vector3(spock.transform.position.x, spock.transform.position.y - (sinkSpeed * Time.deltaTime), spock.transform.position.z);
    }
}
