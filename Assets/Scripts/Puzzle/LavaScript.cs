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
        if (spocks.Count != 0)
        {
            var nullSpocks = new List<int>();
            var spocksInd = 0;
            foreach (var spock in spocks)
            {
                if (spock != null)
                {
                    spock.transform.position = new Vector3(spock.transform.position.x, spock.transform.position.y - (sinkSpeed * Time.deltaTime), spock.transform.position.z);
                    spock.transform.rotation = Quaternion.Slerp(spock.transform.rotation, Quaternion.Euler(0, spock.transform.rotation.eulerAngles.y, 0), sinkRotationSpeed * Time.deltaTime);
                }
                else
                    nullSpocks.Add(spocksInd);
                spocksInd++;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spocks"))
        {
            if (!spocks.Contains(collision.gameObject))
            {
                var newSpock = collision.gameObject;
                newSpock.GetComponent<Rigidbody>().isKinematic = true;
                newSpock.tag = "Lava Spock";
                spocks.Add(newSpock);
                newSpock.transform.position -= new Vector3(0, 0.1f, 0);
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
