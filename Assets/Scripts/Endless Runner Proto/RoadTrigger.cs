using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoadTrigger : MonoBehaviour
{
    public GameObject[] sectionPrefab;
    private int currentIndex = 0;

    public GameObject lastSpawnedObject;
    public float zOffset = 16f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            //Spawn new platform
            Quaternion spawnRotation = lastSpawnedObject != null ? lastSpawnedObject.transform.rotation : Quaternion.identity;

            spawnRotation = Quaternion.Euler(spawnRotation.eulerAngles.x, 0, spawnRotation.eulerAngles.z);

            Vector3 newPosition;


            if (lastSpawnedObject != null)
            {
                newPosition = new Vector3(lastSpawnedObject.transform.position.x, 0, lastSpawnedObject.transform.position.z + zOffset);
            }

            else
            {
                newPosition = new Vector3(transform.position.x, 0, 100);
            }
            
            lastSpawnedObject = Instantiate(sectionPrefab[currentIndex], newPosition, spawnRotation);
            lastSpawnedObject.transform.parent = GameObject.Find("World").transform;

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
