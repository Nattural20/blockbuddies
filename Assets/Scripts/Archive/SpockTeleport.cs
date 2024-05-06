using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpockTeleport : MonoBehaviour
{
    public Transform telePos;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Spocks"))
        {
            other.transform.position = telePos.position;
        }
    }
}
