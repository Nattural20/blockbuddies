using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingLava : MonoBehaviour
{
    public LavaRiseScipt lava;

    public float riseSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            lava.rising = true;
        }
    }
}
