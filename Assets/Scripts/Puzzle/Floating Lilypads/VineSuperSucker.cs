using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSuperSucker : MonoBehaviour
{
    public float sinkSpeed;
    bool isSinking;
    int rotateDir;
    GameObject sinkingSpock;
    public void ActivateVineSuck(GameObject lilyPadSpocks)
    {
        if (isSinking)
        {
            Destroy(sinkingSpock);
            StopAllCoroutines();
        }
        sinkingSpock = Instantiate(lilyPadSpocks, lilyPadSpocks.transform.position, lilyPadSpocks.transform.rotation);
        sinkingSpock.transform.localScale = lilyPadSpocks.transform.lossyScale;
        
        rotateDir = Random.Range(0, 2);
        if (rotateDir == 0)
        {
            rotateDir = -1;
        }

        LockZoneMovingSpocks[] spockDetectors = sinkingSpock.GetComponentsInChildren<LockZoneMovingSpocks>();
        foreach (LockZoneMovingSpocks spock in spockDetectors)
        {
            Destroy(spock);
        }
        isSinking = true;
        StartCoroutine(DestroyDelay());
    }
    private void FixedUpdate()
    {
        if (isSinking)
        {
            sinkingSpock.transform.position -= new Vector3 (0, sinkSpeed * Time.deltaTime, 0);

            sinkingSpock.transform.rotation = Quaternion.Euler (sinkingSpock.transform.rotation.eulerAngles - new Vector3(0, sinkSpeed * 10 * rotateDir * Time.deltaTime, 0));
        }
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(sinkingSpock);
        isSinking = false;
    }
}
