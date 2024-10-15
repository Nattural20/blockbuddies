using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTransparency : MonoBehaviour
{
    void Start()
    {
        var mats = gameObject.GetComponent<Renderer>().material;
        mats.EnableKeyword("_EMISSIONS");
        mats.EnableKeyword("_ALPHABLEND_ON");
    }
}
