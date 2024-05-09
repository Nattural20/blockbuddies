using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : MonoBehaviour
{
    public float activeTime = 1, resetTime = 1, startDelay = 1;

    public GameObject[] geysers;

    public bool acivateStart = false;
    void Start()
    {
        if (acivateStart)
        {
            StartCoroutine(StartExplodeTimer());
        }
    }

    void Explode()
    {
        foreach (GameObject spout in geysers)
        {
            spout.SetActive(true);
        }
    }
    void UnExplode()
    {
        foreach (GameObject spout in geysers)
        {
            spout.SetActive(false);
        }
    }
    IEnumerator StartExplodeTimer()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(ExplodeTimer());
    }
    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(resetTime);
        Explode();
        yield return new WaitForSeconds(activeTime);
        UnExplode();
        StartCoroutine(ExplodeTimer());
    }
    public void DeactivateBubble()
    {
        StopAllCoroutines();
        UnExplode();
    }
    public void ActivateBubble()
    {
        StartCoroutine(ExplodeTimer());
    }
}