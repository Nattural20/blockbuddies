using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineElevatorDetector : MonoBehaviour
{
    public vineElevator vE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        vE. movingGroup.Add(col.transform.gameObject);
    }
}
