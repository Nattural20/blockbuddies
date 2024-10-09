using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMessageRemover : MonoBehaviour
{
    void Update()
    {
        if (Input.GetAxis("DPadVertical") != 0)
            gameObject.SetActive(false);
        else if (Input.GetAxis("DualPadVertical") != 0)
            gameObject.SetActive(false);
    }
}
