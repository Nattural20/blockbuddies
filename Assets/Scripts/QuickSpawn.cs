using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSpawn : MonoBehaviour
{
    [SerializeField] private GameObject TestCube; 
    private static GameObject TheThing;
    // Start is called before the first frame update
    void Start()
    {
        TheThing = TestCube; //spoof
    }

    // Update is called once per frame
    void Update()
    {
        //spawn Thing
        if (Input.GetKeyUp(KeyCode.P))
        {
            SpawnThing(); //Spawn single object
        }
    }

    static void SpawnThing()
    {
        Instantiate(TheThing, new Vector3(1.06f, 1f, 11.29f), Quaternion.identity); //Dummy GameObject- if we get this to spawn we have a successful read 
        Debug.Log("Spawned Thing(Quick Spawn)");
    }
}
