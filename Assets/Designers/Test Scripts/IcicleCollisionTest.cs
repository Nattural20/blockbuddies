using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Icy"))
        {
            Debug.Log("Icicle hit. " + other);
        }
        if (other.CompareTag("Player"))
        {
            Debug.Log("Icicle hit. " + other);
        }
    }
}
