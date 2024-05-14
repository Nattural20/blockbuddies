using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : MonoBehaviour
{
    public float activeTime = 1, resetTime = 1, startDelay = 1;

    public GameObject[] geysers;

    Animator shake;

    public bool acivateStart = false;
    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            shake = GetComponent<Animator>();
}
        if (acivateStart)
        {
            StartCoroutine(StartExplodeTimer());
        }
    }
    IEnumerator StartExplodeTimer()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(ExplodeTimer());
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
    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(resetTime / 2);
        if (shake != null)
        {
            shake.SetBool("isStartWobble", true);
        }
        yield return new WaitForSeconds(resetTime / 2);
        Explode();
        yield return new WaitForSeconds(activeTime);
        UnExplode();
        if (shake != null)
        {
            shake.SetBool("isStartWobble", false);
        }
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
