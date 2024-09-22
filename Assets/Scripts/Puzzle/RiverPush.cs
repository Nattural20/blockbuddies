using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverPush : MonoBehaviour
{
    public float riverSpeed = 1, playerForcePush;
    public List<GameObject> spocks;
    void Start()
    {
        spocks = new List<GameObject>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Spocks"))
        {
            if (!spocks.Contains(collision.gameObject))
            {
                FindAnyObjectByType<AudioManager>().Play("SpockInVines"); //Sound effect script- this line plays a sound from the AudioManager.
                FindAnyObjectByType<AudioManager>().Play("SinkingInVines"); //Sound effect script- this line plays a sound from the AudioManager.
                var newSpock = collision.gameObject.AddComponent<RiverSpockScript>();
                newSpock.tag = "River Spock";
                newSpock.GetPushDirection(transform.forward, riverSpeed);
            }
        }
        else if (collision.gameObject.CompareTag("Body"))
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerInVines"); //Sound effect script- this line plays a sound from the AudioManager.
        }
    }
}
