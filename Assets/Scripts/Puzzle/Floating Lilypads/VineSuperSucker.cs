using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSuperSucker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lily Pad"))
        {
            foreach (GameObject ghost in collision.gameObject.GetComponentInParent<LilyPadLockedSpawn>().ghostSpawnCubes)
            {
                ghost.GetComponent<MeshRenderer>().enabled = false;
                ghost.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
