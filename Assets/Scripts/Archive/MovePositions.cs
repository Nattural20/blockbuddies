using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositions : MonoBehaviour
{
    public GameObject cubeBottom;
    public GameObject cubeTop;

    public Transform pos1;
    public Transform pos2;
    void Start()
    {
        cubeBottom.transform.position = pos1.position;
        cubeTop.transform.position = pos2.position;
    }
}
