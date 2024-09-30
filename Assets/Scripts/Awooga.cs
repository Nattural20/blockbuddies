using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awooga : MonoBehaviour
{
    Vector3 startScale;
    bool awoogaed;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (!awoogaed)
            {
                transform.localScale = startScale + new Vector3(0, 10, 0);
                awoogaed = !awoogaed;
            }
            else 
            {
                transform.localScale = startScale;
                awoogaed = !awoogaed;
            }
        }
    }
}
