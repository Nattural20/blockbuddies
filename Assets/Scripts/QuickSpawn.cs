using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuickSpawn : MonoBehaviour
{
    [SerializeField] private GameObject TestCube; 
    private static GameObject TheThing;

    private int arrayPos;

    public TextMeshProUGUI text;

    public Transform spawnPos;
    public PhysicMaterial[] blockPhys;
    public Material[] blockMaterials;
    public GameObject roots;

    public List<GameObject> spawnedBlocks;
    // Start is called before the first frame update
    void Start()
    {
        TheThing = TestCube; //spoof
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            arrayPos += 1;
            if (arrayPos == 3)
                arrayPos = 0;
            UpdateText();
            Debug.Log(arrayPos);
        }
        //spawn Thing
        if (Input.GetKeyUp(KeyCode.P) || Input.GetKeyUp(KeyCode.Mouse0))
        {
            SpawnThing(); //Spawn single object
        }
    }
    void UpdateText()
    {
        if (arrayPos == 0)
            text.text = "Normal";
        if (arrayPos == 1)
            text.text = "Bouncy";
        if (arrayPos == 2)
            text.text = "Rooted";
    }
    void SpawnThing()
    {
        var newBlock = Instantiate(TheThing, spawnPos.position, spawnPos.rotation); //Dummy GameObject- if we get this to spawn we have a successful read 
        newBlock.GetComponent<BoxCollider>().material = blockPhys[arrayPos];
        newBlock.GetComponent<MeshRenderer>().material = blockMaterials[arrayPos];
        if (arrayPos == 2)
        {
             var root = newBlock.AddComponent<RootedBlock>();
            root.roots = roots;
        }
        spawnedBlocks.Add(newBlock);
        if (spawnedBlocks.Count > 3)
        {
            Destroy(spawnedBlocks[0]);
            spawnedBlocks.RemoveAt(0);
        }
        Debug.Log("Spawned Thing(Quick Spawn)");
    }
}
