using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerToggleObjs : MonoBehaviour
{
    public GameObject[] gameObjectsToggle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Body"))
        {
            foreach (GameObject obj in gameObjectsToggle)
            {
                obj.SetActive(!obj.activeSelf);
            }
            Destroy(this);
        }
    }
}
