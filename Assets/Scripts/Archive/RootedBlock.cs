using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootedBlock : MonoBehaviour
{
    public GameObject roots;
    Rigidbody rb;

    public bool isRooted = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRooted)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                rb.isKinematic = true;
                Instantiate(roots, transform);
                isRooted = true;
            }
        }
    }
}
