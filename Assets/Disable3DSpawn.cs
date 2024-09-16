using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable3DSpawn : MonoBehaviour
{
    public ArduinoReader readerScript; //use this to get reference to the real
    public SpawnerCodeV3 spawnScript; //use this to get reference to spawner script, should pass in object with the script

    //GhostBuster
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(UnityEngine.Collider collision) //When the player enters, set player to true. 
    {
        if (collision.gameObject.tag == "Body")
        {

            spawnScript.canSpawnSpocks = false;
            Debug.Log("Locking SpawnScript");
        }
    }

    private void OnTriggerExit(Collider collision) //When the player exits, set to false. 
    {
        if (collision.gameObject.tag == "Body")
        {

            spawnScript.canSpawnSpocks = true; //fixed for v3 spawn code spoof

        }
    }

}
