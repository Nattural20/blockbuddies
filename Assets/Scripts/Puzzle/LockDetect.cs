using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDetect : MonoBehaviour
{
    public GameObject key;
    public bool activated = false;

    BoxCollider lider;

    void Start()
    {
        lider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (key != null)
        //{
        //    key.transform.position = Vector3.Lerp(key.transform.position, transform.position, 5 * Time.deltaTime);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            key = other.gameObject;
            activated = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == key)
        {
            key = null;
            activated = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (key != null)
        {
            key.transform.position = Vector3.Lerp(key.transform.position, transform.position + lider.center, 5 * Time.deltaTime);
        }
    }
}
