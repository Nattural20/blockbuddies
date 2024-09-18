using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField]
    private bool hasPlayed = false;
    public string soundOut;
    public string soundIn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(UnityEngine.Collider collision) //When the player enters, set player to true. 
    {
        if ((collision.gameObject.tag == "Body") & !hasPlayed)
        {
            FindAnyObjectByType<AudioManager>().CrossFade(soundOut, soundIn, 4);
            hasPlayed = true;
        }
    }
}
