using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaTouchBlow : MonoBehaviour
{
    public GameObject lavaBubble;
    private Animator lavaWobble;

    public float timeToBust = 1f;

    private GameObject bustingSpock;
    private void Start()
    {
        lavaWobble = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lava Spock"))
        {

            StartCoroutine(BustingTime());
        }
    }
    IEnumerator BustingTime()
    {
        float elapsedTime = 0;

        while (elapsedTime < timeToBust)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(Color.white, Color.red, elapsedTime / timeToBust);
            yield return null;
        }
    }
}
