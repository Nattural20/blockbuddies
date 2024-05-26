using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRiseSetNewPos : MonoBehaviour
{
    public LavaRiseScipt lava;

    public float newHeightReset;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            lava.resetPosition = new Vector3(0, newHeightReset, 0);
        }
    }
}
