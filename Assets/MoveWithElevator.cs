using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithElevator : MonoBehaviour
{

    public vineElevator vE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //foreach (var obj in movingGroup)
        //{
        if (vE.rising)
        {
            this.transform.Translate(0, vE.elevatorSpeed * Time.fixedDeltaTime, 0);
        }
        else
        {
            this.transform.Translate(0, -vE.elevatorSpeed * Time.fixedDeltaTime, 0);
        }

    }

}
