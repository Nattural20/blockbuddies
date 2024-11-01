using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverPush : MonoBehaviour
{
    public float riverSpeed = 1, lerpSpeed;
    public List<GameObject> spocks;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spocks"))
        {
            if (collision.gameObject.GetComponent<RiverSpockScript>() == null)
            {
                FindAnyObjectByType<AudioManager>().Play("SpockInVines"); //Sound effect script- this line plays a sound from the AudioManager.
                //FindAnyObjectByType<AudioManager>().Play("SinkingInVines"); //Sound effect script- this line plays a sound from the AudioManager.

                var newSpock = collision.gameObject.AddComponent<RiverSpockScript>();
                newSpock.tag = "River Spock";
                newSpock.GetPushDirection(transform.forward, riverSpeed);
            }
        }
        else if (collision.gameObject.CompareTag("River Spock"))
        {
                var newSpock = collision.gameObject.GetComponent<RiverSpockScript>();
                newSpock.NewPushDirection(transform.forward, riverSpeed, lerpSpeed);
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerInWater"); //Sound effect script- this line plays a sound from the AudioManager.
        }
    }
}
