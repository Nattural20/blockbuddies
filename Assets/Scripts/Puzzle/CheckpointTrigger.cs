using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public ParticleSystem Bellpart;
    public Animator Bellanim;
    bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered)
        {
            if (other.CompareTag("Body"))
            {
                Bellanim.SetTrigger("Checkpoint");
                Bellpart.Play();
                hasTriggered = true;
            }
        }
    }
}
