using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public GameObject fallingPlatform;
    public float secondsBeforeDestroy = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Body")
        {
            fallingPlatform.GetComponent<Animator>().SetBool("isFalling", true);
            StartCoroutine(StartCountDown());
        }
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(secondsBeforeDestroy);
        Destroy(fallingPlatform);
    }

}
