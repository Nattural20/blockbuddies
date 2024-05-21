using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public float sinkSpeed, sinkRotationSpeed;

    public List<GameObject> spocks;
    void Start()
    {
        spocks = new List<GameObject>();
    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spocks"))
        {
            if (!spocks.Contains(collision.gameObject))
            {
                var newSpock = collision.gameObject.AddComponent<LavaSpockScript>();
                //newSpock.GetComponent<Rigidbody>().isKinematic = true;
                newSpock.tag = "Lava Spock";
                //spocks.Add(newSpock);
                //newSpock.transform.position -= new Vector3(0, 0.1f, 0);
                newSpock.sinkSpeed = sinkSpeed;
                newSpock.sinkRotationSpeed = sinkRotationSpeed;
            }
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            GetComponent<AudioSource>().Play();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Spocks"))
        //{
        //    spocks.Remove(collision.gameObject);
        //    collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //}
    }
}
