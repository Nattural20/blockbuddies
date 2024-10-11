using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineRespawnReset : MonoBehaviour
{
    public LavaRiseScipt jkVine;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            jkVine.ResetVines();
        }
    }
}
