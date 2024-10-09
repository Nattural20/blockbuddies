using System;
using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject fallingPlatform;
    public GameObject[] fallingPlatformChild;
    public float secondsBeforeFall = 3;

    private int index;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.tag == "Body")
        {
            FindAnyObjectByType<AudioManager>().Play("PlatformFallWarning"); //Sound effect script- this line plays a sound from the AudioManager.
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
            yield return null;
        }

        fallingPlatform.GetComponent<Animator>().SetBool("isFalling", true);
        fallingPlatform.GetComponent<Animator>().SetBool("isStartWobble", false);
        FindAnyObjectByType<AudioManager>().Play("PlatformFall"); //Sound effect script- this line plays a sound from the AudioManager.
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        // After falling, wait 5 seconds before destroy
        
        yield return new WaitForSeconds(5);
            
        Animator animator = fallingPlatform.GetComponent<Animator>();
        animator.SetBool("isStartWobble", false);    
        animator.SetBool("isFalling", false);
            // Get the specific falling platform child
            

        GameObject fallingPlatformChildren = fallingPlatformChild[index]; 
        fallingPlatformChildren.transform.localPosition = new Vector3(0, 0, 0);
    }
}
