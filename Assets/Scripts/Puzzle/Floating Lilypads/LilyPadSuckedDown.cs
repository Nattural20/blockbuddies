using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class LilyPadSuckedDown : MonoBehaviour
{
    public float suckSpeed;
    public float resetTime;
    bool suckedDown;
    Vector3 floatPos, suckedPos;
    private void Start()
    {
        floatPos = transform.localPosition;
        suckedPos = floatPos + new Vector3(2, 0, 0);
    }
    private void FixedUpdate()
    {
        if (suckedDown)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, suckedPos, suckSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, floatPos, suckSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lily Pad Sucker"))
        {
            suckedDown = true;
            StartCoroutine(ResetSuck());
        }
    }
    IEnumerator ResetSuck()
    {
        yield return new WaitForSeconds(resetTime);
        suckedDown = false;
    }
}
