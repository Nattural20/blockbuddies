using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class risingLava : MonoBehaviour
{

    public GameObject lava;
    bool rising;
    public float riseSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rising)
        {
            lava.transform.Translate(0, riseSpeed * Time.fixedDeltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            rising = true;
        }
    }
}
