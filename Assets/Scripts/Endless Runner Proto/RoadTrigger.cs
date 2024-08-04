using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    public GameObject[] sectionPrefab;
    private int currentIndex = 0;

    public GameObject lastSpawnedObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            //Spawn new platform
            Quaternion spawnRotation = lastSpawnedObject != null ? lastSpawnedObject.transform.rotation : Quaternion.identity;

            //Need to debug the rotation identity for spinning mechanic
            lastSpawnedObject = Instantiate(sectionPrefab[currentIndex], new Vector3(0,0, 16), spawnRotation);

            //Move to next object in array
            currentIndex++;

            //Reset array when exhausted
            if (currentIndex == sectionPrefab.Length)
            {
                currentIndex = 0;
            }
        }
    }
}
