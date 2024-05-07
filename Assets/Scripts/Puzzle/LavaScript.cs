using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public float sinkSpeed;

    private List<GameObject> spocks;
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
                    spock.transform.position = new Vector3(spock.transform.position.x, spock.transform.position.y - (sinkSpeed * Time.deltaTime), spock.transform.position.z);
                else
                    nullSpocks.Add(spocksInd);
                spocksInd++;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spocks"))
        {
            var newSpock = collision.gameObject;
            newSpock.GetComponent<Rigidbody>().isKinematic = true;
            newSpock.tag = "Untagged";
            spocks.Add(newSpock);
        }
    }
}
