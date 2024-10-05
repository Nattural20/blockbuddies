using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationalBehaviourYAxis : MonoBehaviour
{
    [SerializeField] public Dropdown spawnRotation;
    public readonly string[] options = { "Left", "Up", "Right", "Down" };

    Vector3 startRotation;
    private void Start()
    {
        startRotation = gameObject.transform.localRotation.eulerAngles;
    }
    void Update()
    {
        if (spawnRotation.value == 0)
        {
            gameObject.transform.localRotation = Quaternion.Euler(startRotation);
        }
        else if (spawnRotation.value == 1)
        {
            gameObject.transform.localRotation = Quaternion.Euler(startRotation + new Vector3(0, -90, 0));
        }
        else if (spawnRotation.value == 2)
        {
            gameObject.transform.localRotation = Quaternion.Euler(startRotation + new Vector3(0, -180, 0));
        }
        else if (spawnRotation.value == 3)
        {
            gameObject.transform.localRotation = Quaternion.Euler(startRotation + new Vector3(0, -270, 0));
        }
    }

}