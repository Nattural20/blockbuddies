using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject fallingPlatform;
    public GameObject fallingPlatformChild;
    public float secondsBeforeFall = 3;

    public Renderer platformRenderer;  

    void Start()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            fallingPlatform.GetComponent<Animator>().SetBool("isStartWobble", true);
            StartCoroutine(StartWobble());
        }
    }

    IEnumerator StartWobble()
    {
        float elapsedTime = 0;

        while (elapsedTime < secondsBeforeFall)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(Color.white, Color.red, elapsedTime / secondsBeforeFall);
            platformRenderer.material.color = newColor;
            yield return null; 
        }

        fallingPlatform.GetComponent<Animator>().SetBool("isFalling", true);
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        // After falling, wait 5 seconds before destroy
        yield return new WaitForSeconds(5);

        //restart object
        fallingPlatform.GetComponent<Animator>().SetBool("isStartWobble", false);
        fallingPlatform.GetComponent<Animator>().SetBool("isFalling", false);
        platformRenderer.material.color = Color.white;
        fallingPlatformChild.transform.localPosition = new Vector3(0, 0, 0);
    }
}
