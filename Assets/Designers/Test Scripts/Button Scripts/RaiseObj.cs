using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseObj : MonoBehaviour
{
    public float riseHeight;
    public float riseSpeed;

    public GameObject obj;

    bool activated = false;
    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Activate());
                activated = true;
            }
        }
    }
    public IEnumerator Activate()
    {
        var heightRise = obj.transform.position + new Vector3 (0, riseHeight, 0);
        while (obj.transform.position != heightRise)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, heightRise, riseSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
