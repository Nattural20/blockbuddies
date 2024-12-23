using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class LilyPadSuckedDown : MonoBehaviour
{
    public float suckSpeed;
    public float resetTime;
    bool suckedDown, suckDestroy;
    Vector3 floatPos, suckedPos;
    public float suckDestroySpeed;
    float suckAmount;
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

        if (suckDestroy)
        {
            transform.position += new Vector3(suckDestroySpeed * Time.deltaTime, -suckSpeed * Time.deltaTime);
            suckAmount += suckSpeed * Time.deltaTime;

            if (suckAmount > suckSpeed)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lily Pad Sucker"))
        {
            if (!suckedDown)
            {
                suckedDown = true;
                StartCoroutine(ResetSuck());
            }
        }
    }
    IEnumerator ResetSuck()
    {
        yield return new WaitForSeconds(resetTime);
        suckedDown = false;
    }
    public void SuckAndDestroy()
    {
        suckDestroy = true;
    }
}
