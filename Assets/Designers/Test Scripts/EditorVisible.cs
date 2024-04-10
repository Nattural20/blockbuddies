using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorVisible : MonoBehaviour
{
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
