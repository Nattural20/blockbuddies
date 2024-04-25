
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Activate());
    }
    public IEnumerator Activate()
    {
        yield return null;
    }
}
