using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationalBehaviour : MonoBehaviour
{
    [SerializeField] public Dropdown spawnRotation;
    public readonly string[] options = { "Left", "Up", "Right", "Down" };

    void Update()
    {
        if (spawnRotation.value == 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (spawnRotation.value == 1)
        {
            gameObject.transform.rotation = new Quaternion(-90, 0, 0, 0);
        }
        else if (spawnRotation.value == 2)
        {
            gameObject.transform.rotation = new Quaternion(-180, 0, 0, 0);
        }
        else if (spawnRotation.value == 3)
        {
            gameObject.transform.rotation = new Quaternion(-270, 0, 0, 0);
        }
    }

}
