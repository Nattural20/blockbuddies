using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public ParticleSystem Bellpart;
    public Animator Bellanim;

    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Body"))
        {
            Bellanim.SetTrigger("Checkpoint");
            Bellpart.Play();
        }
    }
}
