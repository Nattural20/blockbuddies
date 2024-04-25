using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleCollisionTest : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Body"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position) * 10, ForceMode.Impulse);
        }
    }
}
