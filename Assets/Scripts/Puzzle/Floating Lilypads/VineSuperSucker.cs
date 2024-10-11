using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSuperSucker : MonoBehaviour
{
    public float sinkSpeed, rotateSpeed;
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
        Destroy(sinkingSpock.GetComponent<RotationalBehaviourYAxis>());
        foreach (LockZoneMovingSpocks spockZone in sinkingSpock.GetComponentsInChildren<LockZoneMovingSpocks>())
        {
            Destroy(spockZone);
            var ani = spockZone.GetComponent<Animator>();
            ani.StopPlayback();
            Destroy(ani);
        }
        
        rotateDir = Random.Range(0, 2);
        if (rotateDir == 0)
        {
            rotateDir = -1;
        }

        isSinking = true;
        StartCoroutine(DestroyDelay());
    }
    private void FixedUpdate()
    {
        if (isSinking)
        {
            sinkingSpock.transform.position -= new Vector3 (0, sinkSpeed * Time.deltaTime, 0);

            sinkingSpock.transform.rotation = Quaternion.Euler(sinkingSpock.transform.rotation.eulerAngles - new Vector3(0, sinkSpeed * 10 * rotateDir * Time.deltaTime, 0));
        }

        var rotation = new Vector3(0, 0, sinkSpeed * rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles - rotation);
    }
    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(sinkingSpock);
        isSinking = false;
    }
}
